using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.DTO;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IStore<SubjectDto> _subjects;
        private readonly IStore<MissedLecturesDto> _lectures;
        private readonly IStore<ClassDto> _classes;
        private readonly IStore<PersonDto> _persons;
        private readonly IMapper _mapper;

        public SubjectService(IStore<SubjectDto> subjects, IStore<MissedLecturesDto> lectures, IStore<ClassDto> classes, IStore<PersonDto> persons, IMapper mapper)
        {
            _subjects = subjects;
            _lectures = lectures;
            _classes = classes;
            _persons = persons;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(Subject subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            if (string.IsNullOrEmpty(subject.Name))
            {
                throw new ArgumentException(nameof(subject.Name));
            }

            var dto = _mapper.Map<SubjectDto>(subject);
            await _subjects.AddAsync(dto);
            return dto.Id;
        }

        public async Task DeleteAsync(int subjectId)
        {
            var hasRecords =  _lectures
               .GetAll()
               .Any(lecture => lecture.SubjectId == subjectId);

            if (hasRecords)
            {
                throw new InvalidOperationException("Student has related records");
            }

            await _subjects.DeleteAsync(subjectId);
        }

        public async Task<Subject> GetByIdAsync(int subjectId)
        {
            var result = await _subjects.GetByIdAsync(subjectId);

            if (result == null)
            {
                throw new ArgumentException("Subject not found");
            }

            var model = _mapper.Map<Subject>(result);

            return model;
        }

        public IAsyncEnumerable<Subject> GetAll()
        {
            var dtos = _subjects.GetAll().AsAsyncEnumerable();
            var models = _mapper.Map<IAsyncEnumerable<Subject>>(dtos);

            return models;
        }

        public  async Task UpdateAsync(Subject subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            var dto = _mapper.Map<SubjectDto>(subject);
            await _subjects.UpdateAsync(dto);
        }

        public IAsyncEnumerable<Person> GetLecturersAsync(int subjectId)
        {
            var lecturerIds = _classes
                .GetAll()
                .Where(classModel => classModel.SubjectId == subjectId)
                .Select(classModel => classModel.LecturerId);

            var lecturers = from lecturer in _persons.GetAll()
                            where !lecturer.IsStudent
                            join id in lecturerIds on lecturer.Id equals id
                            select lecturer;

            var models = _mapper.Map<IAsyncEnumerable<Person>>(lecturers.AsAsyncEnumerable());

            return models;
        }

        public IAsyncEnumerable<Person> GetStudentsAsync(int subjectId)
        {
            var lecturerIds = _classes
                .GetAll()
                .Where(classModel => classModel.SubjectId == subjectId)
                .Select(classModel => classModel.LecturerId);

            var lecturers = from lecturer in _persons.GetAll()
                            where lecturer.IsStudent
                            join id in lecturerIds on lecturer.Id equals id
                            select lecturer;

            var models = _mapper.Map<IAsyncEnumerable<Person>>(lecturers.AsAsyncEnumerable());

            return models;
        }

    }
}

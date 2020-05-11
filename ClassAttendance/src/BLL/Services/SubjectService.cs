using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Dtos;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class SubjectService : ISubjectService
    {
        private readonly IStore<SubjectDto> _subjects;
        private readonly IStore<ClassDto> _classes;
        private readonly IStore<StudentDto> _persons;
        private readonly IMapper _mapper;

        public SubjectService(IStore<SubjectDto> subjects,
            IStore<ClassDto> classes,
            IStore<StudentDto> persons,
            IMapper mapper)
        {
            _subjects = subjects;
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
            var hasRecords =  _classes
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

        public IEnumerable<Subject> GetAll()
        {
            var dtos = _subjects.GetAll().AsEnumerable();
            var models = _mapper.Map<IEnumerable<Subject>>(dtos);

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

        public IEnumerable<Lecturer> GetLecturersAsync(int subjectId)
        {
            var lecturerIds = _classes
                .GetAll()
                .Where(classModel => classModel.SubjectId == subjectId)
                .Select(classModel => classModel.LecturerId);

            var lecturers = from lecturer in _persons.GetAll()
                            join id in lecturerIds on lecturer.Id equals id
                            select lecturer;

            var models = _mapper.Map<IEnumerable<Lecturer>>(lecturers.AsEnumerable());

            return models;
        }

        public IEnumerable<Student> GetStudentsAsync(int subjectId)
        {
            var lecturerIds = _classes
                .GetAll()
                .Where(classModel => classModel.SubjectId == subjectId)
                .Select(classModel => classModel.LecturerId);

            var lecturers = from lecturer in _persons.GetAll()
                            join id in lecturerIds on lecturer.Id equals id
                            select lecturer;

            var models = _mapper.Map<IEnumerable<Student>>(lecturers.AsEnumerable());

            return models;
        }

    }
}

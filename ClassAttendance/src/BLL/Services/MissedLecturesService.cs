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
    public class MissedLecturesService : IMissedLecturesService
    {
        private readonly IStore<MissedLecturesDto> _lectures;
        private readonly IStore<PersonDto> _persons;
        private readonly IStore<SubjectDto> _subjects;
        private readonly IStore<ClassDto> _classes;
        private readonly IMapper _mapper;

        public MissedLecturesService(IStore<MissedLecturesDto> lectures,
            IStore<PersonDto> persons,
            IStore<SubjectDto> subjects,
            IStore<ClassDto> classes,
            IMapper mapper)
        {
            _lectures = lectures;
            _persons = persons;
            _subjects = subjects;
            _classes = classes;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(MissedLectures lecture)
        {
            if (lecture == null)
            {
                throw new ArgumentNullException(nameof(lecture));
            }

            var subject = await _subjects.GetByIdAsync(lecture.SubjectId);

            if (subject == null)
            {
                throw new ArgumentException("Subject not found");
            }

            var student = await _persons.GetByIdAsync(lecture.StudentId);

            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            if (!student.IsStudent)
            {
                throw new ArgumentException("Given id do not belong to student");
            }

            var lesson = await _classes.GetByIdAsync(lecture.ClassId);

            if (lesson == null)
            {
                throw new ArgumentException("Class not found");
            }

            var dto = _mapper.Map<MissedLecturesDto>(lecture);
            await _lectures.AddAsync(dto);

            return dto.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var lecture = await GetByIdAsync(id);

            await _lectures.DeleteAsync(id);
        }

        public async Task UpdateAsync(MissedLectures lecture)
        {
            var lectureToUpdate = await GetByIdAsync(lecture.Id);

            if (lecture == null)
            {
                throw new ArgumentNullException(nameof(lecture));
            }

            var subject = await _subjects.GetByIdAsync(lecture.SubjectId);

            if (subject == null)
            {
                throw new ArgumentException("Subject not found");
            }

            var student = await _persons.GetByIdAsync(lecture.StudentId);

            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            if (!student.IsStudent)
            {
                throw new ArgumentException("Given id do not belong to student");
            }

            var lesson = await _classes.GetByIdAsync(lecture.ClassId);

            if (lesson == null)
            {
                throw new ArgumentException("Class not found");
            }

            var dto = _mapper.Map<MissedLecturesDto>(lecture);
            await _lectures.UpdateAsync(dto);
        }

        public async Task<MissedLectures> GetByIdAsync(int id)
        {
            var lecture = await _lectures.GetByIdAsync(id);

            if (lecture == null)
            {
                throw new ArgumentException("Lecture not found");
            }

            var model = _mapper.Map<MissedLectures>(lecture);

            return model;
        }

        public IAsyncEnumerable<MissedLectures> GetAll()
        {
            var dtos = _lectures.GetAll().AsAsyncEnumerable();
            var models = _mapper.Map<IAsyncEnumerable<MissedLectures>>(dtos);

            return models; 
        }

        public async Task<IAsyncEnumerable<MissedLectures>> GetMissedLecturesByStudentAsync(int studentId)
        {
            var student = await _persons.GetByIdAsync(studentId);

            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            if (!student.IsStudent)
            {
                throw new ArgumentException("Given id do not belong to student");
            }

            var missedLectures = _lectures
                .GetAll()
                .Where(lecture => lecture.StudentId == studentId)
                .AsAsyncEnumerable();

            var models = _mapper.Map<IAsyncEnumerable<MissedLectures>>(missedLectures);

            return models;
        }

        public async Task<IAsyncEnumerable<Person>> GetSlackersAsync(Class classModel)
        {
            var slackerIds = await _lectures
                .GetAll()
                .Where(lecture => lecture.ClassId == classModel.Id)
                .Select(lecture => lecture.StudentId)
                .ToListAsync();

            var slackers = from person in _persons.GetAll()
                           where person.IsStudent
                           join slacker in slackerIds on person.Id equals slacker
                           select person;
            var models = _mapper.Map<IAsyncEnumerable<Person>>(slackers.AsAsyncEnumerable());

            return models;
        }

        public async Task<IAsyncEnumerable<MissedLectures>> GetMissedLecturesByLecturerAsync(int id)
        {
            var lecturer = await _persons.GetByIdAsync(id);

            if (lecturer == null)
            {
                throw new ArgumentException("Lecturer not found");
            }

            if (lecturer.IsStudent)
            {
                throw new ArgumentException("Given id do not belong to student");
            }

            var lectures = from lecture in _lectures.GetAll()
                           join classModel in _classes.GetAll() on lecture.ClassId equals classModel.Id
                           where classModel.LecturerId == id
                           select lecture;

            var models = _mapper.Map<IAsyncEnumerable<MissedLectures>>(lectures.AsAsyncEnumerable());

            return models;
        }
    }
}

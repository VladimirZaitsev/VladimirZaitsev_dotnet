using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Dtos;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MissedClassService : IMissedClassService
    {
        private readonly IStore<MissedLecturesDto> _missedClasses;
        private readonly IStore<PersonDto> _persons;
        private readonly IStore<ClassDto> _classes;
        private readonly IMapper _mapper;

        public MissedClassService(IStore<MissedLecturesDto> missedClasses,
            IStore<PersonDto> persons,
            IStore<SubjectDto> subjects,
            IStore<ClassDto> classes,
            IMapper mapper)
        {
            _missedClasses = missedClasses;
            _persons = persons;
            _classes = classes;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(MissedClass missedClass)
        {
            if (missedClass == null)
            {
                throw new ArgumentNullException(nameof(missedClass));
            }

            var student = await _persons.GetByIdAsync(missedClass.StudentId);

            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            if (student.Status != Status.Student)
            {
                throw new ArgumentException("Given id do not belong to student");
            }

            var lesson = await _classes.GetByIdAsync(missedClass.ClassId);

            if (lesson == null)
            {
                throw new ArgumentException("Class not found");
            }

            var dto = _mapper.Map<MissedLecturesDto>(missedClass);
            await _missedClasses.AddAsync(dto);

            return dto.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var lecture = await GetByIdAsync(id);

            await _missedClasses.DeleteAsync(id);
        }

        public async Task UpdateAsync(MissedClass missedClass)
        {
            var lectureToUpdate = await GetByIdAsync(missedClass.Id);

            if (missedClass == null)
            {
                throw new ArgumentNullException(nameof(missedClass));
            }

            var student = await _persons.GetByIdAsync(missedClass.StudentId);

            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            if (student.Status != Status.Student)
            {
                throw new ArgumentException("Given id do not belong to student");
            }

            var lesson = await _classes.GetByIdAsync(missedClass.ClassId);

            if (lesson == null)
            {
                throw new ArgumentException("Class not found");
            }

            var dto = _mapper.Map<MissedLecturesDto>(missedClass);
            await _missedClasses.UpdateAsync(dto);
        }

        public async Task<MissedClass> GetByIdAsync(int id)
        {
            var missedClass = await _missedClasses.GetByIdAsync(id);

            if (missedClass == null)
            {
                throw new ArgumentException("Lecture not found");
            }

            var model = _mapper.Map<MissedClass>(missedClass);

            return model;
        }

        public IEnumerable<MissedClass> GetAll()
        {
            var dtos = _missedClasses.GetAll().AsEnumerable();
            var models = _mapper.Map<IEnumerable<MissedClass>>(dtos);

            return models; 
        }

        public async Task<IEnumerable<MissedClass>> GetMissedLecturesByStudentAsync(int studentId)
        {
            var student = await _persons.GetByIdAsync(studentId);

            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            if (student.Status != Status.Student)
            {
                throw new ArgumentException("Given id do not belong to student");
            }

            var missedClasses = _missedClasses
                .GetAll()
                .Where(lecture => lecture.StudentId == studentId)
                .AsEnumerable();

            var models = _mapper.Map<IEnumerable<MissedClass>>(missedClasses);

            return models;
        }

        public async Task<IEnumerable<Person>> GetSlackersAsync(Class classModel)
        {
            var slackerIds = await _missedClasses
                .GetAll()
                .Where(lecture => lecture.ClassId == classModel.Id)
                .Select(lecture => lecture.StudentId)
                .ToListAsync();

            var slackers = from person in _persons.GetAll()
                           where person.Status == Status.Student
                           join slacker in slackerIds on person.Id equals slacker
                           select person;
            var models = _mapper.Map<IEnumerable<Person>>(slackers.AsEnumerable());

            return models;
        }

        public async Task<IEnumerable<MissedClass>> GetMissedLecturesByLecturerAsync(int id)
        {
            var lecturer = await _persons.GetByIdAsync(id);

            if (lecturer == null)
            {
                throw new ArgumentException("Lecturer not found");
            }

            if (lecturer.Status != Status.Lecturer)
            {
                throw new ArgumentException("Given id do not belong to lecturer");
            }

            var lectures = from lecture in _missedClasses.GetAll()
                           join classModel in _classes.GetAll() on lecture.ClassId equals classModel.Id
                           where classModel.LecturerId == id
                           select lecture;

            var models = _mapper.Map<IEnumerable<MissedClass>>(lectures.AsEnumerable());

            return models;
        }
    }
}

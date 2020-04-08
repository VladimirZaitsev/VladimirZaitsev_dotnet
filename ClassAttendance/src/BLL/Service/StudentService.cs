using DAL.Interfaces;
using DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BLL.Service
{
    internal class StudentService : IStudentService
    {
        private readonly IStore<Person> _persons;
        private readonly IStore<MissedLectures> _lectures;

        public StudentService(IStore<Person> persons, IStore<MissedLectures> lectures)
        {
            _persons = persons;
            _lectures = lectures;
        }

        public async Task<int> AddStudentAsync(Person student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            if (string.IsNullOrEmpty(student.FirstName))
            {
                throw new ArgumentException(nameof(student.FirstName));
            }

            if (string.IsNullOrEmpty(student.LastName))
            {
                throw new ArgumentException(nameof(student.LastName));
            }

            if (!student.IsStudent)
            {
                throw new ArgumentNullException("Student can't be a lecturer");
            }

            await _persons.AddAsync(student);
            return student.Id;
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            var hasRecords = await _lectures
               .GetAll()
               .AnyAsync(lecture => lecture.StudentId == studentId);

            if (hasRecords)
            {
                throw new InvalidOperationException("Student has related records");
            }

            await _persons.DeleteAsync(studentId);
        }

        public async Task<IAsyncEnumerable<MissedLectures>> GetMissedLecturesAsync(int studentId)
        {
            var student = await GetStudent(studentId);

            var missedLectures = _lectures
                .GetAll()
                .Where(lecture => lecture.StudentId == student.Id)
                .AsAsyncEnumerable();

            return missedLectures;
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

            return slackers.AsAsyncEnumerable();
        }

        public async Task<Person> GetStudent(int studentId)
        {
            var student = await _persons.GetByIdAsync(studentId);

            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            if (!student.IsStudent)
            {
                throw new ArgumentException("Invalid id");
            }

            return student;
        }

        public IAsyncEnumerable<Person> GetStudents()
        {
            return _persons
                .GetAll()
                .Where(person => person.IsStudent)
                .AsAsyncEnumerable();
        }

        public async Task UpdateStudentAsync(Person student)
        {
            var result = await _persons
               .GetAll()
               .FirstOrDefaultAsync(person => person.Id == student.Id);

            if (result == null)
            {
                throw new ArgumentException("Class not found");
            }

            await _persons.UpdateAsync(student);
        }
    }
}

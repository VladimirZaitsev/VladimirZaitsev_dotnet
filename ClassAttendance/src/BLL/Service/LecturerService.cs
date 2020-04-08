using BLL.Interfaces;
using DAL.Domain;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class LecturerService : ILecturerService
    {
        private readonly IStore<MissedLectures> _lectures;
        private readonly IStore<Person> _persons;
        private readonly IStore<Class> _classes;

        public LecturerService(IStore<MissedLectures> lectures, IStore<Person> persons, IStore<Class> classes)
        {
            _lectures = lectures;
            _persons = persons;
            _classes = classes;
        }

        public IAsyncEnumerable<Person> GetAll()
        {
            return _persons.GetAll().Where(person => !person.IsStudent).AsAsyncEnumerable();
        }

        public async Task<int> AddAsync(Person lecturer)
        {
            if (lecturer == null)
            {
                throw new ArgumentNullException(nameof(lecturer));
            }

            if (string.IsNullOrEmpty(lecturer.FirstName))
            {
                throw new ArgumentException(nameof(lecturer.FirstName));
            }

            if (string.IsNullOrEmpty(lecturer.LastName))
            {
                throw new ArgumentException(nameof(lecturer.LastName));
            }

            if (lecturer.IsStudent)
            {
                throw new ArgumentNullException("Lecturer can't be a student");
            }

            await _persons.AddAsync(lecturer);
            return lecturer.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var classes = from classModel in _classes.GetAll()
                          join lecture in _lectures.GetAll() on classModel.Id equals lecture.ClassId
                          select classModel;

            var hasRecords = await classes.AnyAsync(classModel => classModel.LecturerId == id);

            if (hasRecords)
            {
                throw new InvalidOperationException("Student has related records");
            }

            await _persons.DeleteAsync(id);
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var lecturer = await _persons.GetByIdAsync(id);

            if (lecturer == null)
            {
                throw new ArgumentException("Student not found");
            }

            if (lecturer.IsStudent)
            {
                throw new ArgumentException("Invalid id");
            }

            return lecturer;
        }

        public async Task UpdateAsync(Person lecturer)
        {
            if (lecturer == null)
            {
                throw new ArgumentNullException(nameof(lecturer));
            }

            if (string.IsNullOrEmpty(lecturer.FirstName))
            {
                throw new ArgumentException(nameof(lecturer.FirstName));
            }

            if (string.IsNullOrEmpty(lecturer.LastName))
            {
                throw new ArgumentException(nameof(lecturer.LastName));
            }

            if (lecturer.IsStudent)
            {
                throw new ArgumentNullException("Lecturer can't be a student");
            }

            await _persons.UpdateAsync(lecturer);
        }
    }
}

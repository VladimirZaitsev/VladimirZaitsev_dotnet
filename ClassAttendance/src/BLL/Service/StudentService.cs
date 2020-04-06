using DAL.Core;
using DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Service
{
    internal class StudentService : IService<Student>
    {
        private readonly IStore<Student> _students;
        private readonly IStore<MissedLectures> _lectures;

        public StudentService(IStore<Student> students, IStore<MissedLectures> lectures)
        {
            _students = students;
            _lectures = lectures;
        }

        public void Add(Student item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (string.IsNullOrEmpty(item.FirstName))
            {
                throw new ArgumentException(nameof(item.FirstName));
            }

            if (string.IsNullOrEmpty(item.LastName))
            {
                throw new ArgumentException(nameof(item.LastName));
            }

            _students.Add(item);
        }

        public void Delete(int itemId)
        {
            var hasRecords = _lectures
                .GetAll()
                .Any(lecture => lecture.StudentId == itemId);

            if (hasRecords)
            {
                throw new InvalidOperationException("Student has related records");
            }

            _students.Delete(itemId);
        }

        public IEnumerable<Student> GetAll()
        {
            return _students.GetAll();
        }

        public Student GetById(int itemId)
        {
            var result = _students.GetById(itemId);

            if (result == null)
            {
                throw new ArgumentException(nameof(itemId));
            }

            return result;
        }

        public void Update(Student item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _students.Update(item);
        }
    }
}

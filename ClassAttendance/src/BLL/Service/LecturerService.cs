using BLL.Interfaces;
using DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Service
{
    public class LecturerService : IService<Person>
    {
        private readonly IService<MissedLectures> _lectures;
        private readonly IService<Person> _persons;

        public LecturerService(IService<Person> persons)
        {
            _persons = persons;
        }

        public int Add(Person item)
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

            _persons.Add(item);
            return item.Id;
        }

        public void Delete(int itemId)
        {
            var hasRecords = _lectures
               .GetAll()
               .Any(lecture => lecture.LecturerId == itemId);

            if (hasRecords)
            {
                throw new InvalidOperationException("Student has related records");
            }

            _persons.Delete(itemId);
        }

        public IEnumerable<Person> GetAll()
        {
            return _persons.GetAll().Where(person => !person.IsStudent);
        }

        public Person GetById(int itemId)
        {
            return _persons.GetById(itemId);
        }

        public void Update(Person item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _persons.Update(item);
        }
    }
}

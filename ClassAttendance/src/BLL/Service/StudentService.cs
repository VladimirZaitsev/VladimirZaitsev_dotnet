﻿using DAL.Interfaces;
using DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;

namespace BLL.Service
{
    internal class StudentService : IService<Person>
    {
        private readonly IStore<Person> _persons;
        private readonly IStore<MissedLectures> _lectures;

        public StudentService(IStore<Person> persons, IStore<MissedLectures> lectures)
        {
            _persons = persons;
            _lectures = lectures;
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
                .Any(lecture => lecture.StudentId == itemId);

            if (hasRecords)
            {
                throw new InvalidOperationException("Student has related records");
            }

            _persons.Delete(itemId);
        }

        public IEnumerable<Person> GetAll()
        {
            return _persons.GetAll().Where(person => person.IsStudent);
        }

        public Person GetById(int itemId)
        {
            var result = _persons
                .GetAll()
                .FirstOrDefault(person => person.Id == itemId);

            if (result == null)
            {
                throw new ArgumentException("Student not found");
            }

            return result;
        }

        public void Update(Person item)
        {
            var result = _persons
                .GetAll()
                .FirstOrDefault(person => person.Id == item.Id);

            if (result == null)
            {
                throw new ArgumentException("Class not found");
            }

            _persons.Update(item);
        }
    }
}

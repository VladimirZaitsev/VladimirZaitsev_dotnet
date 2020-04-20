using DAL.Interfaces;
using DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BLL.Models;
using AutoMapper;

namespace BLL.Services
{
    internal class StudentService : IService<Person>
    {
        private readonly IStore<PersonDto> _persons;
        private readonly IStore<MissedLecturesDto> _lectures;
        private readonly IMapper _mapper;

        public StudentService(IStore<PersonDto> persons, IStore<MissedLecturesDto> lectures, IMapper mapper)
        {
            _persons = persons;
            _lectures = lectures;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(Person student)
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

            var dto = _mapper.Map<PersonDto>(student);
            await _persons.AddAsync(dto);
            return dto.Id;
        }

        public async Task DeleteAsync(int studentId)
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

        public async Task<Person> GetByIdAsync(int studentId)
        {
            var student = await _persons.GetByIdAsync(studentId);

            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            if (student.Status == Status.Student)
            {
                throw new ArgumentException("Invalid id");
            }

            var dto = _mapper.Map<Person>(student);

            return dto;
        }

        public IAsyncEnumerable<Person> GetAll()
        {
            var dtos = _persons.GetAll().Where(person => person.Status == Status.Student).AsAsyncEnumerable();
            var models = _mapper.Map<IAsyncEnumerable<Person>>(dtos);

            return models;
        }

        public async Task UpdateAsync(Person student)
        {
            var result = await _persons
               .GetAll()
               .FirstOrDefaultAsync(person => person.Id == student.Id);

            if (result == null)
            {
                throw new ArgumentException("Class not found");
            }

            var dto = _mapper.Map<PersonDto>(student);
            await _persons.UpdateAsync(dto);
        }
    }
}

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
    internal class StudentService : IService<Student>
    {
        private readonly IStore<StudentDto> _students;
        private readonly IStore<MissedLecturesDto> _lectures;
        private readonly IMapper _mapper;

        public StudentService(IStore<StudentDto> persons, IStore<MissedLecturesDto> lectures, IMapper mapper)
        {
            _students = persons;
            _lectures = lectures;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(Student student)
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

            var dto = _mapper.Map<StudentDto>(student);
            await _students.AddAsync(dto);
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

            await _students.DeleteAsync(studentId);
        }

        public async Task<Student> GetByIdAsync(int studentId)
        {
            var student = await _students.GetByIdAsync(studentId);

            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            var dto = _mapper.Map<Student>(student);

            return dto;
        }

        public IEnumerable<Student> GetAll()
        {
            var dtos = _students.GetAll().AsEnumerable();
            var models = _mapper.Map<IEnumerable<Student>>(dtos);

            return models;
        }

        public async Task UpdateAsync(Student student)
        {
            var result = await _students
               .GetAll()
               .FirstOrDefaultAsync(person => person.Id == student.Id);

            if (result == null)
            {
                throw new ArgumentException("Class not found");
            }

            var dto = _mapper.Map<StudentDto>(student);
            await _students.UpdateAsync(dto);
        }
    }
}

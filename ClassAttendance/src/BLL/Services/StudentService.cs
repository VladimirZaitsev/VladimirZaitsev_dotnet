using DAL.Interfaces;
using DAL.Dtos;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BLL.Models;
using AutoMapper;
using BLL.Exceptions;

namespace BLL.Services
{
    internal class StudentService : IStudentService
    {
        private readonly IStore<StudentDto> _students;
        private readonly IStore<MissedLecturesDto> _lectures;
        private readonly IStore<GroupDto> _groups;
        private readonly IMapper _mapper;

        public StudentService(IStore<StudentDto> students,
            IStore<MissedLecturesDto> lectures,
            IStore<GroupDto> groups,
            IMapper mapper)
        {
            _students = students;
            _lectures = lectures;
            _groups = groups;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(Student student)
        {
            if (student == null)
            {
                throw new BusinessLogicException(nameof(student));
            }

            if (string.IsNullOrEmpty(student.FirstName))
            {
                throw new BusinessLogicException(nameof(student.FirstName));
            }

            if (string.IsNullOrEmpty(student.LastName))
            {
                throw new BusinessLogicException(nameof(student.LastName));
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
                throw new BusinessLogicException("Student has related records");
            }

            await _students.DeleteAsync(studentId);
        }

        public async Task<Student> GetByIdAsync(int studentId)
        {
            var student = await _students.GetByIdAsync(studentId);

            if (student == null)
            {
                throw new BusinessLogicException("Student not found");
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
                throw new BusinessLogicException("Student not found");
            }

            var dto = _mapper.Map<StudentDto>(student);
            await _students.UpdateAsync(dto);
        }

        public async Task<Group> GetStudentGroupAsync(int studentId)
        {
            var student = await GetByIdAsync(studentId);
            var group =  await _groups.GetByIdAsync(student.GroupId);

            if (group == null)
            {
                throw new BusinessLogicException("Group not found");
            }

            var result = _mapper.Map<Group>(group);

            return result;
        }
    }
}

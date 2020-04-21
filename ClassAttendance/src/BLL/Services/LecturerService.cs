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
    public class LecturerService : ILecturerService
    {
        private readonly IStore<MissedLecturesDto> _missedClasses;
        private readonly IStore<PersonDto> _persons;
        private readonly IStore<ClassDto> _classes;
        private readonly IMapper _mapper;

        public LecturerService(IStore<MissedLecturesDto> missedClasses, IStore<PersonDto> persons, IStore<ClassDto> classes, IMapper mapper)
        {
            _missedClasses = missedClasses;
            _persons = persons;
            _classes = classes;
            _mapper = mapper;
        }

        public IEnumerable<Person> GetAll()
        {
            var dtos = _persons.GetAll().Where(person => person.Status == Status.Lecturer).AsAsyncEnumerable();
            var models = _mapper.Map<IEnumerable<Person>>(dtos);

            return models;
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

            if (lecturer.Status == Status.Student)
            {
                throw new ArgumentNullException("Lecturer can't be a student");
            }

            var dto = _mapper.Map<PersonDto>(lecturer);
            await _persons.AddAsync(dto);
            return dto.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var classes = from classModel in _classes.GetAll()
                          join lecture in _missedClasses.GetAll() on classModel.Id equals lecture.ClassId
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

            if (lecturer.Status == Status.Student)
            {
                throw new ArgumentException("Invalid id");
            }

            var model = _mapper.Map<Person>(lecturer);
            return model;
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

            if (lecturer.Status == Status.Student)
            {
                throw new ArgumentNullException("Lecturer can't be a student");
            }

            var dto = _mapper.Map<PersonDto>(lecturer);
            await _persons.UpdateAsync(dto);
        }
    }
}

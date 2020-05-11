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
    internal class LecturerService : IService<Lecturer>
    {
        private readonly IStore<MissedLecturesDto> _missedClasses;
        private readonly IStore<LecturerDto> _persons;
        private readonly IStore<ClassDto> _classes;
        private readonly IMapper _mapper;

        public LecturerService(IStore<MissedLecturesDto> missedClasses, IStore<LecturerDto> persons, IStore<ClassDto> classes, IMapper mapper)
        {
            _missedClasses = missedClasses;
            _persons = persons;
            _classes = classes;
            _mapper = mapper;
        }

        public IEnumerable<Lecturer> GetAll()
        {
            var dtos = _persons.GetAll().AsEnumerable();
            var models = _mapper.Map<IEnumerable<Lecturer>>(dtos);

            return models;
        }

        public async Task<int> AddAsync(Lecturer lecturer)
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

            var dto = _mapper.Map<LecturerDto>(lecturer);
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

        public async Task<Lecturer> GetByIdAsync(int id)
        {
            var lecturer = await _persons.GetByIdAsync(id);

            if (lecturer == null)
            {
                throw new ArgumentException("Student not found");
            }

            var model = _mapper.Map<Lecturer>(lecturer);
            return model;
        }

        public async Task UpdateAsync(Lecturer lecturer)
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

            var dto = _mapper.Map<LecturerDto>(lecturer);
            await _persons.UpdateAsync(dto);
        }
    }
}

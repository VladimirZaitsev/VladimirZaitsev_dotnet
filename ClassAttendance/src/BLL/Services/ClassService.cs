using BLL.Interfaces;
using DAL.Dtos;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BLL.Models;
using AutoMapper;
using BLL.Exceptions;

namespace BLL.Services
{
    internal class ClassService : IService<Class>
    {
        private readonly IStore<ClassDto> _classes;
        private readonly IStore<MissedLecturesDto> _lectures;
        private readonly IMapper _mapper;

        public ClassService(IStore<ClassDto> classes, IStore<MissedLecturesDto> lectures, IMapper mapper)
        {
            _classes = classes;
            _lectures = lectures;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(Class item)
        {
            if (item == null)
            {
                throw new BusinessLogicException(nameof(item));
            }

            var sameTimeClasses = await _classes.GetAll()
                .Where(lesson => lesson.StartDate < item.EndDate || item.StartDate < lesson.EndDate)
                .ToListAsync();

            var isCabinetTaken = sameTimeClasses
                .Any(lesson => lesson.CabinetId == item.CabinetId);

            if (isCabinetTaken)
            {
                throw new BusinessLogicException("Cabinet is already taken");
            }

            var isLecturerBusy = sameTimeClasses
                .Any(lesson => lesson.LecturerId == item.LecturerId);

            if (isLecturerBusy)
            {
                throw new BusinessLogicException("Lecturer has other class at that time");
            }

            var dto = _mapper.Map<ClassDto>(item);
            await _classes.AddAsync(dto);
            return dto.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var hasRecords = await _lectures.GetAll()
                .AnyAsync(lecture => lecture.ClassId == id);

            if (hasRecords)
            {
                throw new BusinessLogicException("Class has related records");
            }
            var cls = await GetByIdAsync(id);

            await _classes.DeleteAsync(cls.Id);
        }

        public async Task<Class> GetByIdAsync(int id)
        {
            var dto = await _classes.GetAll()
                .FirstOrDefaultAsync(cls => cls.Id == id);

            if (dto == null)
            {
                throw new BusinessLogicException("Class not found");
            }

            var model = _mapper.Map<Class>(dto);
            return model;
        }

        public IEnumerable<Class> GetAll()
        {
            var dtos = _classes.GetAll().AsEnumerable();
            var models = _mapper.Map<IEnumerable<Class>>(dtos);

            return models;
        }

        public async Task UpdateAsync(Class item)
        {
            var lesson = await GetByIdAsync(item.Id);

            if (lesson == null)
            {
                throw new BusinessLogicException("Class not found");
            }

            var dto = _mapper.Map<ClassDto>(item);
            await _classes.UpdateAsync(dto);
        }
    }
}

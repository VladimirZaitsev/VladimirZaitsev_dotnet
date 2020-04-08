using BLL.Interfaces;
using DAL.Domain;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BLL.Service
{
    public class ClassService : IClassService
    {
        private readonly IStore<Class> _classes;
        private readonly IStore<MissedLectures> _lectures;

        public async Task<int> AddAsync(Class item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var sameTimeClasses = await _classes.GetAll()
                .Where(lesson => lesson.Beginning < item.Ending || item.Beginning < lesson.Ending)
                .ToListAsync();

            var isCabinetTaken = sameTimeClasses
                .Any(lesson => lesson.CabinetId == item.CabinetId);

            if (isCabinetTaken)
            {
                throw new ArgumentException("Cabinet is already taken");
            }

            var isLecturerBusy = sameTimeClasses
                .Any(lesson => lesson.LecturerId == item.LecturerId);

            if (isLecturerBusy)
            {
                throw new ArgumentException("Lecturer has other class at that time");
            }

            await _classes.AddAsync(item);
            return item.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var hasRecords = await _lectures.GetAll()
                .AnyAsync(lecture => lecture.ClassId == id);
        }

        public async Task<Class> GetByIdAsync(int id)
        {
            var classModel = await _classes.GetByIdAsync(id);

            if (classModel == null)
            {
                throw new ArgumentException("Class not found");
            }


            return classModel;
        }

        public IAsyncEnumerable<Class> GetAll()
        {
            return _classes.GetAll().AsAsyncEnumerable();
        }

        public async Task UpdateAsync(Class item)
        {
            var result = await _classes
               .GetAll()
               .FirstOrDefaultAsync(item => item.Id == item.Id);

            if (result == null)
            {
                throw new ArgumentException("Class not found");
            }

            await _classes.UpdateAsync(item);
        }
    }
}

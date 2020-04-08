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

        public async Task<int> AddClassAsync(Class classModel)
        {
            if (classModel == null)
            {
                throw new ArgumentNullException(nameof(classModel));
            }

            var sameTimeClasses = await _classes.GetAll()
                .Where(lesson => lesson.Beginning < classModel.Ending || classModel.Beginning < lesson.Ending)
                .ToListAsync();

            var isCabinetTaken = sameTimeClasses
                .Any(lesson => lesson.CabinetId == classModel.CabinetId);

            if (isCabinetTaken)
            {
                throw new ArgumentException("Cabinet is already taken");
            }

            var isLecturerBusy = sameTimeClasses
                .Any(lesson => lesson.LecturerId == classModel.LecturerId);

            if (isLecturerBusy)
            {
                throw new ArgumentException("Lecturer has other class at that time");
            }

            await _classes.AddAsync(classModel);
            return classModel.Id;
        }

        public Task DeleteClassAsync(int classId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Class> GetClassAsync(int classId)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<Class> GetClasses()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateClassAsync(Class classModel)
        {
            throw new System.NotImplementedException();
        }
    }
}

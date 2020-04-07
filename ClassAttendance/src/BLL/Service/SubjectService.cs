using BLL.Interfaces;
using DAL.Domain;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Service
{
    public class SubjectService : IService<Subject>
    {
        private readonly IStore<Subject> _subjects;
        private readonly IStore<MissedLectures> _lectures;

        public SubjectService(IStore<Subject> subjects, IStore<MissedLectures> lectures)
        {
            _subjects = subjects;
            _lectures = lectures;
        }

        public int Add(Subject item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (string.IsNullOrEmpty(item.Name))
            {
                throw new ArgumentException(nameof(item.Name));
            }

            _subjects.Add(item);
            return item.Id;
        }

        public void Delete(int itemId)
        {
            var hasRecords = _lectures
               .GetAll()
               .Any(lecture => lecture.SubjectId == itemId);

            if (hasRecords)
            {
                throw new InvalidOperationException("Student has related records");
            }

            _subjects.Delete(itemId);
        }

        public IEnumerable<Subject> GetAll()
        {
            return _subjects.GetAll();
        }

        public Subject GetById(int itemId)
        {
            var result = _subjects.GetById(itemId);

            if (result == null)
            {
                throw new ArgumentException(nameof(itemId));
            }

            return result;
        }

        public void Update(Subject item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _subjects.Update(item);
        }
    }
}

using BLL.Interfaces;
using DAL.Domain;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Service
{
    public class ClassService : IService<Class>
    {
        private readonly IStore<Class> _classes;
        private readonly IStore<Person> _persons;
        private readonly IStore<Subject> _subjects;
        private readonly IStore<MissedLectures> _lectures;

        public int Add(Class item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var lecturer = _persons
                .GetAll()
                .Where(person => !person.IsStudent)
                .FirstOrDefault(person => person.Id == item.LecturerId);

            if (lecturer == null)
            {
                throw new ArgumentException("Lecturer do not exists");
            }

            var subject = _subjects
               .GetAll()
               .FirstOrDefault(subject => subject.Id == item.SubjectId);

            if (subject == null)
            {
                throw new ArgumentException("Subject do not exists");
            }

            var isTaken = _classes
                .GetAll()
                .Where(classModel => classModel.CabinetId == item.CabinetId)
                .Any(eventModel => eventModel.Beginning < item.Ending || eventModel.Ending > item.Beginning);                .

            _classes.Add(item);
            return item.Id;
        }

        public void Delete(int itemId)
        {
            var hasRecords = _lectures
                .GetAll()
                .Any(lecture => lecture.ClassId == itemId);

            if (hasRecords)
            {
                throw new InvalidOperationException("Class has related records");
            }

            _classes.Delete(itemId);
        }

        public IEnumerable<Class> GetAll()
        {
            return _classes.GetAll();
        }

        public Class GetById(int itemId)
        {
            var result = _classes
                .GetAll()
                .FirstOrDefault(classModel => classModel.Id == itemId);

            if (result == null)
            {
                throw new ArgumentException("Class not found");
            }

            return result;
        }

        public void Update(Class item)
        {
            var result = _classes
                .GetAll()
                .FirstOrDefault(classModel => classModel.Id == item.Id);

            if (result == null)
            {
                throw new ArgumentException("Class not found");
            }

            _classes.Update(item);
        }
    }
}

using BLL.Interfaces;
using DAL.Domain;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly IStore<Subject> _subjects;
        private readonly IStore<MissedLectures> _lectures;
        private readonly IStore<Class> _classes;
        private readonly IStore<Person> _persons;

        public SubjectService(IStore<Subject> subjects, IStore<MissedLectures> lectures)
        {
            _subjects = subjects;
            _lectures = lectures;
        }

        public async Task<int> AddSubjectAsync(Subject subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            if (string.IsNullOrEmpty(subject.Name))
            {
                throw new ArgumentException(nameof(subject.Name));
            }

            await _subjects.AddAsync(subject);
            return subject.Id;
        }

        public async Task DeleteSubjectAsync(int subjectId)
        {
            var hasRecords =  _lectures
               .GetAll()
               .Any(lecture => lecture.SubjectId == subjectId);

            if (hasRecords)
            {
                throw new InvalidOperationException("Student has related records");
            }

            await _subjects.DeleteAsync(subjectId);
        }

        public async Task<IAsyncEnumerable<Person>> GetLecturersAsync(int subjectId)
        {
            var lecturerIds = _classes
                .GetAll()
                .Where(classModel => classModel.SubjectId == subjectId)
                .Select(classModel => classModel.LecturerId);

            var lecturers = from lecturer in _persons.GetAll()
                            where !lecturer.IsStudent
                            join id in lecturerIds on lecturer.Id equals id
                            select lecturer;

            return await lecturers.ToListAsync();
        }

        public async Task<IAsyncEnumerable<Person>> GetStudentsAsync(int subjectId)
        {
            var lecturerIds = _classes
                .GetAll()
                .Where(classModel => classModel.SubjectId == subjectId)
                .Select(classModel => classModel.LecturerId);

            var lecturers = from lecturer in _persons.GetAll()
                            where lecturer.IsStudent
                            join id in lecturerIds on lecturer.Id equals id
                            select lecturer;

            return await lecturers.AsAsyncEnumerable();
        }

        public async Task<Subject> GetSubjectAsync(int subjectId)
        {
            var result = await _subjects.GetByIdAsync(subjectId);

            if (result == null)
            {
                throw new ArgumentException("Subject not found");
            }

            return result;
        }

        public IAsyncEnumerable<Subject> GetSubjectsAsync()
        {
            return _subjects.GetAll().AsAsyncEnumerable();
        }

        public  async Task UpdatedSubjectAsync(Subject subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            await _subjects.UpdateAsync(subject);
        }
    }
}

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
    public class MissedLecturesService : IMissedLecturesService
    {
        private readonly IStore<MissedLectures> _lectures;
        private readonly IStore<Person> _persons;
        private readonly IStore<Subject> _subjects;
        private readonly IStore<Class> _classes;

        public async Task<int> AddAsync(MissedLectures lecture)
        {
            if (lecture == null)
            {
                throw new ArgumentNullException(nameof(lecture));
            }

            var subject = await _subjects.GetByIdAsync(lecture.SubjectId);

            if (subject == null)
            {
                throw new ArgumentException("Subject not found");
            }

            var student = await _persons.GetByIdAsync(lecture.StudentId);

            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            if (!student.IsStudent)
            {
                throw new ArgumentException("Given id do not belong to student");
            }

            var lesson = await _classes.GetByIdAsync(lecture.ClassId);

            if (lesson == null)
            {
                throw new ArgumentException("Class not found");
            }

            await _lectures.AddAsync(lecture);
            return lecture.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var lecture = await GetByIdAsync(id);

            await _lectures.DeleteAsync(id);
        }

        public async Task UpdateAsync(MissedLectures lecture)
        {
            var lectureToUpdate = await GetByIdAsync(lecture.Id);

            if (lecture == null)
            {
                throw new ArgumentNullException(nameof(lecture));
            }

            var subject = await _subjects.GetByIdAsync(lecture.SubjectId);

            if (subject == null)
            {
                throw new ArgumentException("Subject not found");
            }

            var student = await _persons.GetByIdAsync(lecture.StudentId);

            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            if (!student.IsStudent)
            {
                throw new ArgumentException("Given id do not belong to student");
            }

            var lesson = await _classes.GetByIdAsync(lecture.ClassId);

            if (lesson == null)
            {
                throw new ArgumentException("Class not found");
            }

            await _lectures.UpdateAsync(lecture);
        }

        public async Task<MissedLectures> GetByIdAsync(int id)
        {
            var lecture = await _lectures.GetByIdAsync(id);

            if (lecture == null)
            {
                throw new ArgumentException("Lecture not found");
            }

            return lecture;
        }

        public IAsyncEnumerable<MissedLectures> GetAll()
        {
            return _lectures.GetAll().AsAsyncEnumerable();
        }

        public async Task<IAsyncEnumerable<MissedLectures>> GetMissedLecturesByStudentAsync(int studentId)
        {
            var student = await _persons.GetByIdAsync(studentId);

            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            if (!student.IsStudent)
            {
                throw new ArgumentException("Given id do not belong to student");
            }

            var missedLectures = _lectures
                .GetAll()
                .Where(lecture => lecture.StudentId == studentId)
                .AsAsyncEnumerable();

            return missedLectures;
        }

        public async Task<IAsyncEnumerable<Person>> GetSlackersAsync(Class classModel)
        {
            var slackerIds = await _lectures
                .GetAll()
                .Where(lecture => lecture.ClassId == classModel.Id)
                .Select(lecture => lecture.StudentId)
                .ToListAsync();

            var slackers = from person in _persons.GetAll()
                           where person.IsStudent
                           join slacker in slackerIds on person.Id equals slacker
                           select person;

            return slackers.AsAsyncEnumerable();
        }

        public async Task<IAsyncEnumerable<MissedLectures>> GetMissedLecturesByLecturerAsync(int id)
        {
            var lecturer = await _persons.GetByIdAsync(id);

            if (lecturer == null)
            {
                throw new ArgumentException("Lecturer not found");
            }

            if (lecturer.IsStudent)
            {
                throw new ArgumentException("Given id do not belong to student");
            }

            var lectures = from lecture in _lectures.GetAll()
                           join classModel in _classes.GetAll() on lecture.ClassId equals classModel.Id
                           where classModel.LecturerId == id
                           select lecture;

            return lectures.AsAsyncEnumerable();
        }
    }
}

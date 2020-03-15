using System.Collections.Generic;
using System.Linq;
using Lab1.Domain;

namespace Lab1.Business
{
    /// <summary>
    /// Provides methods to work with student collection.
    /// </summary>
    public static class StudentExtension
    {
        /// <summary>
        /// Creates collection with student average result.
        /// </summary>
        /// <param name="students">Student collection.</param>
        /// <returns>StudentResult collection.</returns>
        public static IEnumerable<StudentResult> GetStudentsResult(this IEnumerable<Student> students)
        {
            var studentResults = students
                .Select(student => new StudentResult
                {
                    StudentName = student.LastName,
                    Average = student.Exams.Average(exam => exam.Mark),
                });

            return studentResults;
        }

        /// <summary>
        /// Creates collection with exam average result.
        /// </summary>
        /// <param name="students">Student collection.</param>
        /// <returns>ExamResult collection.</returns>
        public static IEnumerable<ExamResult> GetExamResults(this IEnumerable<Student> students)
        {
            var examResults = students
                .SelectMany(student => student.Exams)
                .GroupBy(exam => exam.Subject)
                .Select(group => new ExamResult
                {
                    Average = group.Average(exam => exam.Mark),
                    Subject = group.Key,
                });

            return examResults;
        }

        /// <summary>
        /// Calculates average mark.
        /// </summary>
        /// <param name="students">Student collection.</param>
        /// <returns>Average mark.</returns>
        public static double GetGroupAverage(this IEnumerable<Student> students)
            => students.SelectMany(student => student.Exams).Average(exam => exam.Mark);
    }
}

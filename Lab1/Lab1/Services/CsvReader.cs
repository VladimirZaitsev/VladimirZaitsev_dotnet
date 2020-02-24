namespace Lab1.Services
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using Lab1.Domain;

    /// <summary>
    /// This class reads student collection from .csv file.
    /// </summary>
    internal static class CsvReader
    {
        /// <summary>
        /// This method reads student collection from given file.
        /// </summary>
        /// <param name="filePath">Path to .csv file.</param>
        /// <returns>Student collection.</returns>
        public static IEnumerable<Student> Read(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);

            var records = new List<Student>();

            csv.Read();
            csv.ReadHeader();

            var subjectNames = csv.Context.HeaderRecord.Skip(3).ToList();

            while (csv.Read())
            {
                var studentInfo = new Student
                {
                    FirstName = csv.GetField<string>(0),
                    LastName = csv.GetField<string>(1),
                    MiddleName = csv.GetField<string>(2),
                };

                var subjects = new List<Exam>();

                subjectNames.ForEach(
                    name => subjects.Add(
                        new Exam()
                        {
                            Subject = name,
                            Mark = csv.GetField<int>(name),
                        }));

                studentInfo.Exams = subjects;

                records.Add(studentInfo);
            }

            return records.ToList();
        }
    }
}

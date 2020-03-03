namespace Lab1.Domain
{
    using System.Collections.Generic;

    /// <summary>
    /// Contains student's fullname and his marks.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Gets or sets first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets middle name.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets student's exam information.
        /// </summary>
        public IReadOnlyCollection<Exam> Exams { get; set; }
    }
}

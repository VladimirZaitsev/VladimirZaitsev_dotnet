using System.Collections.Generic;

namespace Lab1.Domain
{
    /// <summary>
    /// Contains ExamResult collection, StudentResult Collection and average mark.
    /// </summary>
    public class FinalReport
    {
        /// <summary>
        /// Gets or sets examResult collection.
        /// </summary>
        public IEnumerable<ExamResult> ExamResults { get; set; }

        /// <summary>
        /// Gets or sets studentResult colleciton.
        /// </summary>
        public IEnumerable<StudentResult> StudentResults { get; set; }

        /// <summary>
        /// Gets or sets average mark.
        /// </summary>
        public double Average { get; set; }
    }
}

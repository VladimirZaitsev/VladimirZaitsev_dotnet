using System.Collections.Generic;
using System.IO;
using Lab1.Domain;
using OfficeOpenXml;

namespace Lab1.Services
{
    /// <summary>
    /// Excel writer class.
    /// </summary>
    internal class ExcelWriter : IWriter
    {
        private const string WorksheetName = "Sheet {0}";

        private const string StudentsResults = "A1";

        private const string ExamResults = "D1";

        private const string Average = "G1";

        private const string Extension = ".xlsx";

        /// <summary>
        /// This method creates worksheet and writes all date at first cell.
        /// </summary>
        /// <param name="filePath">Path to xlsx file.</param>
        /// <param name="report">Peport DTO.</param>
        public void Write(string filePath, FinalReport report)
        {
            var pathWithExtension = Path.ChangeExtension(filePath, Extension);
            var file = new FileInfo(pathWithExtension);
            using ExcelPackage excelPackage = new ExcelPackage(file);
            var count = excelPackage.Workbook.Worksheets.Count + 1;
            var worksheet = excelPackage.Workbook.Worksheets.Add(string.Format(WorksheetName, count));

            worksheet.Cells[StudentsResults].LoadFromCollection(report.StudentResults);
            worksheet.Cells[ExamResults].LoadFromCollection(report.ExamResults);
            worksheet.Cells[Average].Value = report.Average;
            excelPackage.Save();
        }
    }
}

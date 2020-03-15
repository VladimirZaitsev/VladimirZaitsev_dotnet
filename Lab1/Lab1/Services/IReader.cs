using System.Collections.Generic;
using Lab1.Domain;

namespace Lab1.Services
{
    /// <summary>
    /// Provides methods for reading data from file.
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// Reads student collection from given file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>Student collection.</returns>
        IEnumerable<Student> Read(string filePath);
    }
}

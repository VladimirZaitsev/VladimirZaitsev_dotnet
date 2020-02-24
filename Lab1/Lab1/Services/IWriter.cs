namespace Lab1.Services
{
    using System.Collections.Generic;
    using Lab1.Domain;

    /// <summary>
    /// Provides method for writing collections to file.
    /// </summary>
    public interface IWriter
    {
        /// <summary>
        /// Writes collections to file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <param name="report">Peport DTO.</param>
        void Write(string filePath, FinalReport report);
    }
}

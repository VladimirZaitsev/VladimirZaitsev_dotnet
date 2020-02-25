namespace Lab1.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Text.Json;
    using Lab1.Domain;

    /// <summary>
    /// Provide method for writing in json format.
    /// </summary>
    internal class JsonWriter : IWriter
    {
        private const string Extension = ".json";

        /// <summary>
        /// Writes collections to json file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <param name="report">Peport DTO.</param>
        public void Write(string filePath, FinalReport report)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            var result = JsonSerializer.Serialize(report, options);

            var pathWithExtension = Path.ChangeExtension(filePath, Extension);

            using var writer = new StreamWriter(pathWithExtension, false, Encoding.UTF8);
            writer.WriteLine(result);
        }
    }
}

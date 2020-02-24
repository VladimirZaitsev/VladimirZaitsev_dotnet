using Lab1.Processing;

namespace Lab1
{
    /// <summary>
    /// Program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entrance point.
        /// </summary>
        /// <param name="args">Console args.</param>
        private static void Main(string[] args)
        {
            var processor = new FileProcessor();
            processor.Process(args);
        }
    }
}

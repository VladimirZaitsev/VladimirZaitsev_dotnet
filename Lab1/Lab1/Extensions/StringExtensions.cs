using System;

namespace Lab1.Extensions
{
    /// <summary>
    /// Provides extra string methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts string to int.
        /// </summary>
        /// <param name="input">String to convert.</param>
        /// <returns>Integer value.</returns>
        public static int ToInt(this string input)
        {
            try
            {
                return int.Parse(input);
            }
            catch (FormatException)
            {
                throw;
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace DAL.Extensions
{
    public static class StringExtensions
    {
        public static List<int> ToInts(this string[] array) => array.Select(x => x.ToInt()).ToList();

        public static int ToInt(this string str) => int.Parse(str);
    }
}

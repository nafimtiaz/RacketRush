using System.Collections.Generic;
using System.Linq;

namespace RacketRush.RR.Utils
{
    public static class SanityUtils
    {
        public static bool HasValidLength<T>(this IEnumerable<T> arr, int size)
        {
            return arr != null && arr.Count() == size;
        }
    }
}
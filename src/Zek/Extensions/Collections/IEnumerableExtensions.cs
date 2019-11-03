using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zek.Extensions.Collections
{
    public static class IEnumerableExtensions
    {
        //public static IEnumerable<T> ConvertAll<T>(this IEnumerable en, Converter<object, T> converter)
        //{
        //    var enumerator = en.GetEnumerator();
        //    while (enumerator.MoveNext())
        //    {
        //        var current = enumerator.Current;
        //        yield return converter(current);
        //    }
        //}

        public static void ForEach<T>(this IEnumerable<T> en, Action<T> action)
        {
            foreach (T local in en)
            {
                action(local);
            }
        }


        public static async Task ForEachAsync<T>(this IEnumerable<T> en, Func<T, Task> func)
        {
            foreach (var value in en)
            {
                await func(value);
            }
        }
    }
}

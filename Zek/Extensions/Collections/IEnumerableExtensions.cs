using System;
using System.Collections.Generic;

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
    }
}

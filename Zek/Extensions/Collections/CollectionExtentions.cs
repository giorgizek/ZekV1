using System;
using System.Collections.Generic;
using System.Linq;

namespace Zek.Extensions.Collections
{
    public static class CollectionExtensions
    {
        ///<summary>
        ///	Remove an item from the collection with predicate
        ///</summary>
        ///<param name = "collection"></param>
        ///<param name = "predicate"></param>
        ///<typeparam name = "T"></typeparam>
        ///<exception cref = "ArgumentNullException"></exception>
        public static void RemoveWhere<T>(this ICollection<T> collection, Predicate<T> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            var deleteList = collection.Where(child => predicate(child)).ToList();
            deleteList.ForEach(t => collection.Remove(t));
        }




        public static void AddRange<T>(this ICollection<T> instance, IEnumerable<T> collection)
        {
            foreach (var local in collection)
            {
                instance.Add(local);
            }
        }
    }
}
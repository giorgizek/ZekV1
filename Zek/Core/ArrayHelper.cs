using System;
using System.Collections.Generic;
using System.Linq;

namespace Zek.Core
{
    /// <summary>
    /// მასივზე დამხმარე კლასი.
    /// </summary>
    public static class ArrayHelper
    {
        /// <summary>
        /// აბრუნებს შეერთებული კოლქეციებს (უნიკალურებს).
        /// </summary>
        /// <typeparam name="T">კოლექციის ტიპი.</typeparam>
        /// <param name="firstList"></param>
        /// <param name="array"></param>
        /// <returns>აბრუნებს მხოლოდ უნიკალურ ჩანაწერებს.</returns>
        [Obsolete("გადატანილია Extention-ში GetMerged", false)]
        public static List<T> MergeLists<T>(List<T> firstList, params List<T>[] array)
        {
            var mergedList = new List<T>();
            foreach (var item in firstList)
            {
                if (!mergedList.Contains(item))
                    mergedList.Add(item);
            }

            foreach (var list in array)
            {
                foreach (var item in list)
                {
                    if (!mergedList.Contains(item))
                        mergedList.Add(item);
                }
            }


            return mergedList;
        }
    }
}

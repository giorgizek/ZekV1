using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zek.Data
{
    /// <summary>
    /// გამოიყენება ლინქისთვის რომ წამოიღოს დისტინქტით მნიშვნელობა.
    /// მაგ.: var pc = new PropertyComparer("Code1C");
    /// LINQ.Distinct(pc).ToList();
    /// </summary>
    /// <typeparam name="T">ენთითიდ ტიპი.</typeparam>
    public class PropertyComparer<T> : IEqualityComparer<T>
    {
        private readonly PropertyInfo _propertyInfo;

        /// <summary>
        /// Creates a new instance of PropertyComparer.
        /// </summary>
        /// <param name="propertyName">The name of the property on type T 
        /// to perform the comparison on.</param>
        public PropertyComparer(string propertyName)
        {
            //store a reference to the property info object for use during the comparison
            _propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            if (_propertyInfo == null)
            {
                throw new ArgumentException($"{propertyName} is not a property of type {typeof (T)}.");
            }
        }

        #region IEqualityComparer<T> Members

        public bool Equals(T x, T y)
        {
            //get the current value of the comparison property of x and of y
            var xValue = _propertyInfo.GetValue(x, null);
            var yValue = _propertyInfo.GetValue(y, null);

            //if the xValue is null then we consider them equal if and only if yValue is null
            if (xValue == null)
                return yValue == null;

            //use the default comparer for whatever type the comparison property is.
            return xValue.Equals(yValue);
        }

        public int GetHashCode(T obj)
        {
            //get the value of the comparison property out of obj
            var propertyValue = _propertyInfo.GetValue(obj, null);

            if (propertyValue == null)
                return 0;

            return propertyValue.GetHashCode();
        }

        #endregion
    }
}

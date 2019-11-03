using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using Zek.Core;
using System.Reflection;
using Zek.Utils;

namespace Zek.Extensions.Linq
{
    public static class LinqExtensions
    {
        public static void ModifyEach<T>(this IList<T> source, Func<T, T> projection)
        {
            for (var i = 0; i < source.Count; i++)
            {
                source[i] = projection(source[i]);
            }
        }


        public static DataTable ToDataTable<T>(this IEnumerable<T> source)
        {
            var result = new DataTable();

            // column names
            PropertyInfo[] props = null;

            if (source == null) return result;

            foreach (var rec in source)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (props == null)
                {
                    props = rec.GetType().GetProperties();
                    foreach (var pi in props)
                    {
                        var colType = pi.PropertyType;

                        if (colType.IsGenericType && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        result.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                var row = result.NewRow();

                foreach (var pi in props)
                {
                    row[pi.Name] = pi.GetValue(rec, null) ?? DBNull.Value;
                }


                result.Rows.Add(row);
            }
            return result;
        }

        //public static List<T> ToList<T>(this DataTable table)
        //{
        //    if (table == null) return null;

        //    var result = new List<T>();

        //    var properties = typeof(T).GetProperties().Where(p => p.CanWrite).ToArray();
        //    var map = new Dictionary<string, int>();
        //    for (var i = 0; i < table.Columns.Count; i++)
        //    {
        //        var prop = properties.FirstOrDefault(x => string.Equals(x.Name, table.Columns[i].ColumnName.Replace(" ", string.Empty), StringComparison.InvariantCultureIgnoreCase));
        //        if (prop == null || map.ContainsKey(prop.Name)) continue;
        //        map.Add(prop.Name, i);
        //    }
        //    if (map.Count == 0) return null;
        //    properties = properties.Where(x => map.ContainsKey(x.Name)).ToArray();

        //    var limit = table.Rows.Count;
        //    Parallel.For(0, limit, rowIndex =>
        //    {
        //        var obj = typeof (T) != typeof (string) ? Activator.CreateInstance<T>() : (object) null;
        //        if (properties.Length > 0)
        //        {
        //            for (var i = 0; i < properties.Length; i++)
        //            {
        //                var value = table.Rows[rowIndex][map[properties[i].Name]];
        //                try
        //                {
        //                    if (value != null && value != DBNull.Value)
        //                        properties[i].SetValue(obj, ConvertHelper.ChangeType(value, properties[i].PropertyType), null);
        //                }
        //                catch (InvalidCastException ex)
        //                {
        //                    throw new InvalidCastException(string.Format("Can't change type (RowIndex: '{0}', ColumnIndex: '{1}', Property: '{2}', ConversionType: '{3}', Value: '{4}').", rowIndex, map[properties[i].Name], properties[i].Name, properties[i].PropertyType, (value != null ? value.ToString() : string.Empty)), ex);
        //                }
        //                catch (FormatException ex)
        //                {
        //                    throw new FormatException(string.Format("Can't change type (RowIndex: '{0}', ColumnIndex: '{1}', Property: '{2}', ConversionType: '{3}', Value: '{4}').", rowIndex, map[properties[i].Name], properties[i].Name, properties[i].PropertyType, (value != null ? value.ToString() : string.Empty)), ex);
        //                }
        //            }
        //        }
        //        else //if it struct list (ex: List<int>);
        //        {
        //            //obj = (T)dr[0];
        //            obj = table.Rows[rowIndex][0];
        //        }

        //        result.Add((T) obj);
        //    });
        //    return result;
        //}
        public static List<T> ToList<T>(this DataTable table)
        {
            if (table == null) return null;

            var result = new List<T>();

            var properties = typeof(T).GetProperties().Where(p => p.CanWrite).ToArray();
            var map = new Dictionary<string, int>();
            for (var i = 0; i < table.Columns.Count; i++)
            {
                var prop = properties.FirstOrDefault(x => string.Equals(x.Name, table.Columns[i].ColumnName.Replace(" ", string.Empty), StringComparison.InvariantCultureIgnoreCase));
                if (prop == null || map.ContainsKey(prop.Name)) continue;
                map.Add(prop.Name, i);
            }
            if (map.Count == 0) return null;
            properties = properties.Where(x => map.ContainsKey(x.Name)).ToArray();


            for (var rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
            {
                var obj = typeof(T) != typeof(string) ? Activator.CreateInstance<T>() : (object)null;
                if (properties.Length > 0)
                {
                    for (var i = 0; i < properties.Length; i++)
                    {
                        var value = table.Rows[rowIndex][map[properties[i].Name]];
                        try
                        {
                            if (value != null && value != DBNull.Value)
                                properties[i].SetValue(obj, ConvertHelper.ChangeType(value, properties[i].PropertyType), null);
                        }
                        catch (InvalidCastException ex)
                        {
                            throw new InvalidCastException($"Can't change type (RowIndex: '{rowIndex}', ColumnIndex: '{map[properties[i].Name]}', Property: '{properties[i].Name}', ConversionType: '{properties[i].PropertyType}', Value: '{(value != null ? value.ToString() : string.Empty)}').", ex);
                        }
                        catch (FormatException ex)
                        {
                            throw new FormatException($"Can't change type (RowIndex: '{rowIndex}', ColumnIndex: '{map[properties[i].Name]}', Property: '{properties[i].Name}', ConversionType: '{properties[i].PropertyType}', Value: '{(value != null ? value.ToString() : string.Empty)}').", ex);
                        }
                    }
                }
                else//if it struct list (ex: List<int>);
                {
                    //obj = (T)dr[0];
                    obj = table.Rows[rowIndex][0];
                }

                result.Add((T)obj);
            }
            return result;
        }

        public static IEnumerable<T> WrapInEnumerable<T>(this T item)
        {
            return new[] { item };
        }
        public static List<T> WrapInList<T>(this T item)
        {
            return new List<T>(new[] { item });
        }
        public static T[] WrapInArray<T>(this T item)
        {
            return new[] { item };
        }

        public static IQueryable<TSource> Between<TSource, TKey>(this IQueryable<TSource> source, TKey key, Expression<Func<TSource, TKey>> lowSelector, Expression<Func<TSource, TKey>> highSelector) where TKey : IComparable<TKey>
        {
            var low = lowSelector.Body;
            var high = highSelector.Body;

            var lowerBound = Expression.LessThanOrEqual(low, Expression.Constant(key));
            var upperBound = Expression.LessThanOrEqual(Expression.Constant(key), high);

            var lowLambda = Expression.Lambda<Func<TSource, bool>>(lowerBound, lowSelector.Parameters);
            var highLambda = Expression.Lambda<Func<TSource, bool>>(upperBound, highSelector.Parameters);

            return source.Where(lowLambda).Where(highLambda);
        }
        public static IQueryable<TSource> Between<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, TKey low, TKey high) where TKey : IComparable<TKey>
        {
            var expression = selector.Body;

            var lowerBound = Expression.GreaterThanOrEqual(expression, Expression.Constant(low));
            var upperBound = Expression.LessThanOrEqual(expression, Expression.Constant(high));

            var lowLambda = Expression.Lambda<Func<TSource, bool>>(lowerBound, selector.Parameters);
            var highLambda = Expression.Lambda<Func<TSource, bool>>(upperBound, selector.Parameters);

            return source.Where(lowLambda).Where(highLambda);
        }
        public static IQueryable<TSource> Overlap<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> lowSelector, Expression<Func<TSource, TKey>> highSelector, TKey low, TKey high) where TKey : IComparable<TKey>
        {
            var lowExpression = lowSelector.Body;
            var lowerBound = Expression.GreaterThanOrEqual(lowExpression, Expression.Constant(low));
            var upperBound = Expression.LessThanOrEqual(lowExpression, Expression.Constant(high));
            var predicateBody1 = Expression.And(lowerBound, upperBound);
            var lambda1 = Expression.Lambda<Func<TSource, bool>>(predicateBody1, lowSelector.Parameters);


            var highExpression = highSelector.Body;
            lowerBound = Expression.GreaterThanOrEqual(highExpression, Expression.Constant(low));
            upperBound = Expression.LessThanOrEqual(highExpression, Expression.Constant(high));
            var predicateBody2 = Expression.And(lowerBound, upperBound);
            var lambda2 = Expression.Lambda<Func<TSource, bool>>(predicateBody2, highSelector.Parameters);


            lowerBound = Expression.LessThanOrEqual(lowExpression, Expression.Constant(low));
            upperBound = Expression.GreaterThanOrEqual(highExpression, Expression.Constant(high));
            var predicateBody3 = Expression.And(lowerBound, upperBound);
            var lambda3 = Expression.Lambda<Func<TSource, bool>>(predicateBody3, highSelector.Parameters);

            return source.Where(lambda1.Or(lambda2).Or(lambda3));
        }
        //public static IQueryable<TSource> NotOverlap<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> lowSelector, Expression<Func<TSource, TKey>> highSelector, TKey low, TKey high) where TKey : IComparable<TKey>
        //{
        //    var lowExpression = lowSelector.Body;
        //    var lowerBound = Expression.GreaterThanOrEqual(lowExpression, Expression.Constant(low));
        //    var upperBound = Expression.LessThanOrEqual(lowExpression, Expression.Constant(high));
        //    var predicateBody1 = Expression.And(lowerBound, upperBound);
        //    var lambda1 = Expression.Lambda<Func<TSource, bool>>(predicateBody1, lowSelector.Parameters);


        //    var highExpression = highSelector.Body;
        //    lowerBound = Expression.GreaterThanOrEqual(highExpression, Expression.Constant(low));
        //    upperBound = Expression.LessThanOrEqual(highExpression, Expression.Constant(high));
        //    var predicateBody2 = Expression.And(lowerBound, upperBound);
        //    var lambda2 = Expression.Lambda<Func<TSource, bool>>(predicateBody2, highSelector.Parameters);


        //    return source.Where(lambda1.Or(lambda2));
        //}
    }
}

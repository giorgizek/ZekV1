using System;
using System.Linq;
using System.Linq.Expressions;
using Zek.Data;
using Zek.Extensions;

namespace Zek.Linq
{
    public static class QueryableFilterExtensions
    {

        public static IQueryable<TSource> Filter<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, WhereOperator whereOperator, TKey value1, TKey value2 = default(TKey), bool filterIfDefault = false)
        {
            if (!filterIfDefault && value1.IsDefault())
            {
                return source;
            }

            switch (whereOperator)
            {
                case WhereOperator.Equals:
                    return source.Equal(selector, value1);

                case WhereOperator.NotEquals:
                    return source.NotEqual(selector, value1);

                case WhereOperator.GreaterThan:
                    return source.GreaterThan(selector, value1);

                case WhereOperator.GreaterThanOrEquals:
                    return source.GreaterThanOrEqual(selector, value1);

                case WhereOperator.LessThan:
                    return source.LessThan(selector, value1);

                case WhereOperator.LessThanOrEquals:
                    return source.LessThanOrEqual(selector, value1);

                case WhereOperator.Between:
                    return source.Between(selector, value1, value2);

                case WhereOperator.Contains:
                    return source.Contains(selector, value1);

                case WhereOperator.NotContains:
                    return source.NotContains(selector, value1);

                case WhereOperator.Begins:
                    return source.Begins(selector, value1);

                case WhereOperator.Ends:
                    return source.Ends(selector, value1);
            }

            return source;
        }
    }
}

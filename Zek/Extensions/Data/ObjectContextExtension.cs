using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
//using System.Data.EntityClient;
//using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace Zek.Extensions.Data
{
    public static class ObjectContextExtension
    {
        public static T ExecuteScalar<T>(this ObjectContext context, string commandText)
        {
            var connection = ((EntityConnection)context.Connection).StoreConnection;
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.Text;

            return (T)cmd.ExecuteScalar();
        }



        public static List<T> GetBy<T>(this ObjectContext ctx, Expression<Func<T, bool>> exp, Expression<Func<T, T>> columns)
    where T : class
        {
            return ctx.CreateQuery<T>($"[{typeof (T).Name}]").Where(exp).Select(columns).ToList();
        }

        public static List<TU> GetBy<T, TU>(this ObjectContext ctx, Expression<Func<T, bool>> exp, Expression<Func<T, TU>> columns)
            where T : class
            where TU : class
        {
            return ctx.CreateQuery<T>($"[{typeof (T).Name}]").Where(exp).Select(columns).ToList();
        }






        //public static List<U> GetBy<T, U>(Expression<Func<T, bool>> exp, Expression<Func<T, U>> columns)
        //    where T : class
        //    where U : class
        //{
        //    using (var ctx = new TravelEntities())
        //    {
        //        return ctx.CreateObjectSet<T>().Where(exp).Select<T, U>(columns).ToList();
        //    }
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Zek.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class// , IDisposable
    {
        IQueryable<TEntity> GetAll();

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null, int? pageSize = null);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);


        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity First(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        //TEntity FirstOrDefault();

        //int Count();
        //int Count(Expression<Func<TEntity, bool>> predicate);
        //bool Any();
        //bool Any(Expression<Func<TEntity, bool>> predicate);



        void Add(TEntity entity);
        void Update(TEntity entityToUpdate);
        void Detach(TEntity entity);
        void Remove(Expression<Func<TEntity, bool>> predicate);
        void Remove(TEntity entity);


        void Save();

        //void Dispose();




        Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        //Task<bool> AnyAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        ////IAsyncEnumerable<TEntity> AsAsyncEnumerable(IQueryable<TEntity> source);
        ////IQueryable<TEntity> AsNoTracking<TEntity>(this IQueryable<TEntity> source) where TEntity : class;
        ////IQueryable<TEntity> AsTracking<TEntity>(this IQueryable<TEntity> source) where TEntity : class;
        ////Task<decimal> AverageAsync(this IQueryable<decimal> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<double> AverageAsync(this IQueryable<double> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<double> AverageAsync(this IQueryable<int> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<double> AverageAsync(this IQueryable<long> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<decimal?> AverageAsync(this IQueryable<decimal?> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<double?> AverageAsync(this IQueryable<double?> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<double?> AverageAsync(this IQueryable<int?> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<double?> AverageAsync(this IQueryable<long?> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<float?> AverageAsync(this IQueryable<float?> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<float> AverageAsync(IQueryable<float> source, CancellationToken cancellationToken = default(CancellationToken));


        //Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<double> AverageAsync(Expression<Func<TEntity, double>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<double> AverageAsync(Expression<Func<TEntity, int>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<double> AverageAsync(Expression<Func<TEntity, long>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<decimal?> AverageAsync(Expression<Func<TEntity, decimal?>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<double?> AverageAsync(Expression<Func<TEntity, double?>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<double?> AverageAsync(Expression<Func<TEntity, int?>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<double?> AverageAsync(Expression<Func<TEntity, long?>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<float?> AverageAsync(Expression<Func<TEntity, float?>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<float> AverageAsync(Expression<Func<TEntity, float>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<bool> ContainsAsync(TEntity item, CancellationToken cancellationToken = default(CancellationToken));
        //Task<int> CountAsync(CancellationToken cancellationToken = default(CancellationToken));
        //Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        //Task<TEntity> FirstAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        //Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        ////Task ForEachAsync<T>(this IQueryable<T> source, Action<T> action, CancellationToken cancellationToken = default(CancellationToken));
        ////IIncludableQueryable<TEntity, TProperty> Include<TEntity, TProperty>(this IQueryable<TEntity> source, Expression<Func<TEntity, TProperty>> navigationPropertyPath) where TEntity : class;
        //Task<TEntity> LastAsync(CancellationToken cancellationToken = default(CancellationToken));
        //Task<TEntity> LastAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        //Task<TEntity> LastOrDefaultAsync(CancellationToken cancellationToken = default(CancellationToken));
        //Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        ////void Load<TEntity>(this IQueryable<TEntity> source);
        ////Task LoadAsync(CancellationToken cancellationToken = default(CancellationToken));
        //Task<long> LongCountAsync(CancellationToken cancellationToken = default(CancellationToken));
        //Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        //Task<TEntity> MaxAsync(CancellationToken cancellationToken = default(CancellationToken));
        //Task<TResult> MaxAsync<TResult>( Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<TEntity> MinAsync(CancellationToken cancellationToken = default(CancellationToken));
        //Task<TResult> MinAsync<TResult>( Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<TEntity> SingleAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        //Task<TEntity> SingleOrDefaultAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<decimal> SumAsync(this IQueryable<decimal> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<double> SumAsync(this IQueryable<double> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<int> SumAsync(this IQueryable<int> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<long> SumAsync(this IQueryable<long> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<decimal?> SumAsync(this IQueryable<decimal?> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<double?> SumAsync(this IQueryable<double?> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<int?> SumAsync(this IQueryable<int?> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<long?> SumAsync(this IQueryable<long?> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<float?> SumAsync(this IQueryable<float?> source, CancellationToken cancellationToken = default(CancellationToken));
        ////Task<float> SumAsync(this IQueryable<float> source, CancellationToken cancellationToken = default(CancellationToken));
        //Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<double> SumAsync(Expression<Func<TEntity, double>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<int> SumAsync(Expression<Func<TEntity, int>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<long> SumAsync(Expression<Func<TEntity, long>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<decimal?> SumAsync(Expression<Func<TEntity, decimal?>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<double?> SumAsync(Expression<Func<TEntity, double?>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<int?> SumAsync(Expression<Func<TEntity, int?>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<long?> SumAsync(Expression<Func<TEntity, long?>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<float?> SumAsync(Expression<Func<TEntity, float?>> selector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<float> SumAsync(Expression<Func<TEntity, float>> selector, CancellationToken cancellationToken = default(CancellationToken));
        ////IIncludableQueryable<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(this IIncludableQueryable<TEntity, ICollection<TPreviousProperty>> source, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath) where TEntity : class;
        ////IIncludableQueryable<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(this IIncludableQueryable<TEntity, TPreviousProperty> source, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath) where TEntity : class;
        //Task<TEntity[]> ToArrayAsync(CancellationToken cancellationToken = default(CancellationToken));
        //Task<Dictionary<TKey, TEntity>> ToDictionaryAsync<TKey>(Func<TEntity, TKey> keySelector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<Dictionary<TKey, TEntity>> ToDictionaryAsync<TKey>(Func<TEntity, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default(CancellationToken));
        //Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>( Func<TEntity, TKey> keySelector, Func<TEntity, TElement> elementSelector, CancellationToken cancellationToken = default(CancellationToken));
        //Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>( Func<TEntity, TKey> keySelector, Func<TEntity, TElement> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default(CancellationToken));
        //Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken = default(CancellationToken));



        Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}

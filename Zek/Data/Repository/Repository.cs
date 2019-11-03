using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Zek.Data.Repository
{


    public class Repository
    {
        public Repository(IDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            Context = context;
        }
        protected IDbContext Context;


        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only

                //try
                //{
                //    if (_objectContext != null && _objectContext.Connection.State == ConnectionState.Open)
                //    {
                //        _objectContext.Connection.Close();
                //    }
                //}
                //catch (ObjectDisposedException)
                //{
                //    // do nothing, the objectContext has already been disposed
                //}

                if (Context != null)
                {
                    Context.Dispose();
                    Context = null;
                }
            }

            _disposed = true;
        }
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


    public class Repository<TEntity> : Repository, IRepository<TEntity> where TEntity : class//, IDisposable
    {
        public Repository(IDbContext context) : base(context)
        {
            _dbSet = Context.Set<TEntity>();
        }

        //todo private readonly IDbSet<TEntity> _dbSet;
        private readonly DbSet<TEntity> _dbSet;



        protected virtual string GetConnectionString()
        {
            //context.Database.AsSqlServer().Connection.DbConnection;
            return (Context as DbContext).Database.Connection.ConnectionString;
        }

        public virtual IQueryable<TEntity> GetAll() => _dbSet;



        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            IQueryable<TEntity> query = _dbSet;

            includeProperties?.ForEach(includeProperty => query.Include(includeProperty));

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return query.ToList();
        }

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate);

        public virtual TEntity Single(Expression<Func<TEntity, bool>> whereCondition) => _dbSet.Single(whereCondition);
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> whereCondition) => _dbSet.SingleOrDefault(whereCondition);

        public virtual TEntity First(Expression<Func<TEntity, bool>> predicate) => _dbSet.First(predicate);
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate) => _dbSet.FirstOrDefault(predicate);

        public virtual void Add(TEntity entity) => _dbSet.Add(entity);

        public virtual void Update(TEntity entityToUpdate)
        {
            //todo სანახავია ეს უნდა თუ არა _dbSet.Attach(entityToUpdate); http://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
            _dbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual void Detach(TEntity entity) => Context.Entry(entity).State = EntityState.Detached;

        public virtual void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in _dbSet.Where(predicate))
            {
                Remove(entity);
            }
        }
        public virtual void Remove(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }




        public virtual void Save() => Context.SaveChanges();
        

        public Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AllAsync(predicate, cancellationToken);
        //public Task<bool> AnyAsync(CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AnyAsync(cancellationToken);
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AnyAsync(predicate, cancellationToken);
        //public Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AverageAsync(selector, cancellationToken);
        //public Task<double> AverageAsync(Expression<Func<TEntity, double>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AverageAsync(selector, cancellationToken);
        //public Task<double> AverageAsync(Expression<Func<TEntity, int>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AverageAsync(selector, cancellationToken);
        //public Task<double> AverageAsync(Expression<Func<TEntity, long>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AverageAsync(selector, cancellationToken);
        //public Task<decimal?> AverageAsync(Expression<Func<TEntity, decimal?>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AverageAsync(selector, cancellationToken);
        //public Task<double?> AverageAsync(Expression<Func<TEntity, double?>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AverageAsync(selector, cancellationToken);
        //public Task<double?> AverageAsync(Expression<Func<TEntity, int?>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AverageAsync(selector, cancellationToken);
        //public Task<double?> AverageAsync(Expression<Func<TEntity, long?>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AverageAsync(selector, cancellationToken);
        //public Task<float?> AverageAsync(Expression<Func<TEntity, float?>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AverageAsync(selector, cancellationToken);
        //public Task<float> AverageAsync(Expression<Func<TEntity, float>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AverageAsync(selector, cancellationToken);
        //public Task<bool> ContainsAsync(TEntity item, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.ContainsAsync(item, cancellationToken);
        //public Task<int> CountAsync(CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.CountAsync(cancellationToken);
        //public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.CountAsync(predicate, cancellationToken);
        //public Task<TEntity> FirstAsync(CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.FirstAsync(cancellationToken);
        public Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.FirstAsync(predicate, cancellationToken);
        //public Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.FirstOrDefaultAsync(cancellationToken);
        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        //public Task<TEntity> LastAsync(CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.LastAsync(cancellationToken);
        //public Task<TEntity> LastAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.LastAsync(predicate, cancellationToken);
        //public Task<TEntity> LastOrDefaultAsync(CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.LastOrDefaultAsync(cancellationToken);
        //public Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.LastOrDefaultAsync(predicate, cancellationToken);
        //public Task<long> LongCountAsync(CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.LongCountAsync(cancellationToken);
        //public Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.LongCountAsync(predicate, cancellationToken);
        //public Task<TEntity> MaxAsync(CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.MaxAsync(cancellationToken);
        //public Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.MaxAsync(selector, cancellationToken);
        //public Task<TEntity> MinAsync(CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.MinAsync(cancellationToken);
        //public Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.MinAsync(selector, cancellationToken);
        //public Task<TEntity> SingleAsync(CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.SingleAsync(cancellationToken);
        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.SingleAsync(predicate, cancellationToken);
        //public Task<TEntity> SingleOrDefaultAsync(CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.SingleOrDefaultAsync(cancellationToken);
        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.SingleOrDefaultAsync(predicate, cancellationToken);
        //public Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.SumAsync(selector, cancellationToken);
        //public Task<double> SumAsync(Expression<Func<TEntity, double>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.SumAsync(selector, cancellationToken);
        //public Task<int> SumAsync(Expression<Func<TEntity, int>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.SumAsync(selector, cancellationToken);
        //public Task<long> SumAsync(Expression<Func<TEntity, long>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.SumAsync(selector, cancellationToken);
        //public Task<decimal?> SumAsync(Expression<Func<TEntity, decimal?>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.SumAsync(selector, cancellationToken);
        //public Task<double?> SumAsync(Expression<Func<TEntity, double?>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.SumAsync(selector, cancellationToken);
        //public Task<int?> SumAsync(Expression<Func<TEntity, int?>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.SumAsync(selector, cancellationToken);
        //public Task<long?> SumAsync(Expression<Func<TEntity, long?>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.SumAsync(selector, cancellationToken);
        //public Task<float?> SumAsync(Expression<Func<TEntity, float?>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.SumAsync(selector, cancellationToken);
        //public Task<float> SumAsync(Expression<Func<TEntity, float>> selector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.SumAsync(selector, cancellationToken);
        //public Task<TEntity[]> ToArrayAsync(CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.ToArrayAsync(cancellationToken);
        //public Task<Dictionary<TKey, TEntity>> ToDictionaryAsync<TKey>(Func<TEntity, TKey> keySelector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.ToDictionaryAsync(keySelector, cancellationToken);
        //public Task<Dictionary<TKey, TEntity>> ToDictionaryAsync<TKey>(Func<TEntity, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.ToDictionaryAsync(keySelector, comparer, cancellationToken);
        //public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>(Func<TEntity, TKey> keySelector, Func<TEntity, TElement> elementSelector, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.ToDictionaryAsync(keySelector, elementSelector, cancellationToken);
        //public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>(Func<TEntity, TKey> keySelector, Func<TEntity, TElement> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.ToDictionaryAsync(keySelector, elementSelector, comparer, cancellationToken);
        //public Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.ToListAsync(cancellationToken);

        public virtual Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken)) => Context.SaveChangesAsync(cancellationToken);






    }
}


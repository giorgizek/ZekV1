using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Zek.Data.Repository
{
    public interface IDbContext
    {
        //todo IDbSet<T> Set<T>() where T : class;
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        DbEntityEntry Entry(object o);
        void Dispose();
    }
}

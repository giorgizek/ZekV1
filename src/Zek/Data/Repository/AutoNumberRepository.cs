using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Zek.Model.Dictionary;

namespace Zek.Data.Repository
{
    public interface IAutoNumberRepository : IRepository<AutoNumber>
    {
        Task<int> GetNextNumberAsync(string name, int currentNumber = 0);
    }

    public class AutoNumberRepository : Repository<AutoNumber>, IAutoNumberRepository
    {
        public AutoNumberRepository(IDbContext context) : base(context)
        {
        }

        public async Task<int> GetNextNumberAsync(string name, int currentNumber = 0)
        {
            var autoNumber = await Where(a => a.Name == name).SingleOrDefaultAsync();
            if (autoNumber == null)//if not exist for this name
            {
                autoNumber = new AutoNumber { Name = name, Number = currentNumber + 1 };
                Add(autoNumber);
            }
            else
            {
                if (currentNumber > autoNumber.Number)
                    autoNumber.Number = currentNumber + 1;
                else
                    autoNumber.Number++;
            }
            await SaveAsync();

            return autoNumber.Number;
        }
    }
}

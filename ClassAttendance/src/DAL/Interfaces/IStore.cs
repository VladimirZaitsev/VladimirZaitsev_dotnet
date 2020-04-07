using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IStore<T>
        where T : class
    {
        Task<T> GetByIdAsync(int itemId);

        Task<int> AddAsync(T item);

        Task DeleteAsync(int itemId);

        Task UpdateAsync(T item);

        IQueryable<T> GetAllAsync();
    }
}

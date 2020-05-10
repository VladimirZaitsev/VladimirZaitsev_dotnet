using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IService<T>
    {
        Task<int> AddAsync(T item);

        Task DeleteAsync(int id);

        Task UpdateAsync(T item);

        Task<T> GetByIdAsync(int id);

        IEnumerable<T> GetAll();
    }
}

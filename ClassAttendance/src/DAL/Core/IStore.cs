using System.Linq;

namespace DAL.Core
{
    public interface IStore<T>
        where T : class
    {
        T GetById(int itemId);

        int Add(T item);

        void Delete(int itemId);

        void Update(T item);

        IQueryable<T> GetAll();
    }
}

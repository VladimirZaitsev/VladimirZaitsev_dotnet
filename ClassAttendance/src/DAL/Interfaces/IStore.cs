using System.Linq;

namespace DAL.Interfaces
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

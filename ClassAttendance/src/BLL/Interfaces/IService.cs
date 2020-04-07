using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IService<T>
        where T : class
    {
        T GetById(int itemId);

        int Add(T item);

        void Delete(int itemId);

        void Update(T item);

        IEnumerable<T> GetAll();
    }
}

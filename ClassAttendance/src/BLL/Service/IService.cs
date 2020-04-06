using System.Collections.Generic;

namespace BLL.Service
{
    public interface IService<T>
        where T : class
    {
        T GetById(int itemId);

        void Add(T item);

        void Delete(int itemId);

        void Update(T item);

        IEnumerable<T> GetAll();
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Core
{
    internal class Store<T> : IStore<T>
         where T : class, IEntity
    {
        private readonly DbContext _context;

        public Store(DbContext context)
        {
            _context = context;
        }

        public int Add(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();

            return item.Id;
        }

        public void Delete(int itemId)
        {
            var itemToRemove = _context.Set<T>().First(item => item.Id == itemId);
            _context.Set<T>().Remove(itemToRemove);
            _context.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            var items = _context.Set<T>()
                .AsNoTracking()

            return items;
        }

        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public T GetById(int itemId)
        {
            var result = _context.Set<T>()
                  .FirstOrDefault(item => item.Id == itemId);

            _context.Entry(result).State = EntityState.Detached;

            return result;
        }
    }
}

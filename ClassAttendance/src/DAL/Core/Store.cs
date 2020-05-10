using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Core
{
    public class Store<T> : IStore<T>
         where T : class, IEntity
    {
        private readonly ApplicationContext _context;

        public Store(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();

            return item.Id;
        }

        public async Task DeleteAsync(int itemId)
        {
            var itemToRemove = await _context.Set<T>().FirstAsync(item => item.Id == itemId);
            _context.Set<T>().Remove(itemToRemove);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            var items = _context.Set<T>()
                .AsNoTracking();

            return items;
        }

        public async Task UpdateAsync(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int itemId)
        {
            var result = await _context.Set<T>()
                  .FirstOrDefaultAsync(item => item.Id == itemId);

            _context.Entry(result).State = EntityState.Detached;

            return result;
        }
    }
}

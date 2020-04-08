using DAL.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IClassService
    {
        IAsyncEnumerable<Class> GetClasses();

        Task<Class> GetClassAsync(int classId);

        Task<int> AddClassAsync(Class classModel);

        Task DeleteClassAsync(int classId);

        Task UpdateClassAsync(Class classModel);
    }
}

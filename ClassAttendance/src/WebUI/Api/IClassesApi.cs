using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    public interface IClassesApi
    {
        [Get("Classes/Classes")]
        Task<List<Class>> GetClasses();

        [Get("Classes/Class")]
        Task<Class> GetClassAsync(int id);

        [Post("Classes/Add")]
        Task<int> CreateClassAsync([Body] Class item);

        [Put("Classes/Update")]
        Task UpdateClassAsync([Body] Class item);

        [Delete("Classes/Delete")]
        Task DeleteClassAsync(int id);
    }
}

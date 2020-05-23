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

        [Post("Classes/Class")]
        Task<int> CreateClassAsync([Body] Class item);

        [Put("Classes/Class")]
        Task UpdateClassAsync([Body] Class item);

        [Delete("Classes/Class")]
        Task DeleteClassAsync(int id);
    }
}

using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    public interface IClassesApi
    {
        [Get("Classes")]
        Task<List<Class>> GetClasses();

        [Get("Classes/{id}")]
        Task<Class> GetClassAsync([Path] int id);

        [Post("Classes")]
        Task<int> CreateClassAsync([Body] Class item);

        [Put("Classes")]
        Task UpdateClassAsync([Body] Class item);

        [Delete("Classes")]
        Task DeleteClassAsync(int id);
    }
}

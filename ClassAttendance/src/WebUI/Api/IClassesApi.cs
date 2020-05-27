using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    public interface IClassesApi
    {
        [Get("Classes")]
        Task<List<Class>> GetAll();

        [Get("Classes/{id}")]
        Task<Class> GetByIdAsync([Path] int id);

        [Post("Classes")]
        Task<int> AddAsync([Body] Class item);

        [Put("Classes")]
        Task UpdateAsync([Body] Class item);

        [Delete("Classes")]
        Task DeleteAsync(int id);
    }
}

using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    [AllowAnyStatusCode]
    public interface ISubjectApi
    {
        [Get("Subjects")]
        Task<List<Subject>> GetAll();

        [Get("Subjects/{id}")]
        Task<Subject> GetByIdAsync([Path] int id);

        [Post("Subjects")]
        Task<int> AddAsync([Body] Subject subject);

        [Put("Subjects")]
        Task UpdateAsync([Body] Subject subject);

        [Delete("Subjects")]
        Task DeleteAsync(int id);
    }
}

using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    [AllowAnyStatusCode]
    public interface IStudentApi
    {
        [Get("Students")]
        Task<List<Student>> GetAll();

        [Get("Students/{id}")]
        Task<Student> GetByIdAsync([Path] int id);

        [Post("Students")]
        Task<int> AddAsync([Body] Student student);

        [Put("Students")]
        Task UpdateAsync([Body] Student student);

        [Delete("Students")]
        Task DeleteAsync(int id);

        [Get("Students/StudentGroup")]
        Task<Group> GetStudentGroupAsync(int id);
    }
}

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
        Task<List<Student>> GetStudents();

        [Get("Students/{id}")]
        Task<Student> GetStudentAsync([Path] int id);

        [Post("Students")]
        Task<int> AddStudentAsync([Body] Student student);

        [Put("Students")]
        Task UpdateStudentAsync([Body] Student student);

        [Delete("Students")]
        Task DeleteStudentAsync(int id);

        [Get("Students/StudentGroup")]
        Task<Group> GetStudentGroupAsync(int id);
    }
}

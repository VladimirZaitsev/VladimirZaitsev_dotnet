using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    [AllowAnyStatusCode]
    public interface IStudentApi
    {
        [Get("Students/Students")]
        Task<List<Student>> GetStudents();

        [Get("Students/Student")]
        Task<Student> GetStudentAsync(int id);

        [Post("Students/Add")]
        Task<int> AddStudentAsync([Body] Student student);

        [Put("Students/Update")]
        Task UpdateStudentAsync([Body] Student student);

        [Delete("Students/Delete")]
        Task DeleteStudentAsync(int id);

        [Get("Students/StudentGroup")]
        Task<Group> GetStudentGroupAsync(int id);
    }
}

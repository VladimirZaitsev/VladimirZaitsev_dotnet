using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    public interface IGroupApi
    {
        [Get("Groups")]
        Task<List<Group>> GetAll();

        [Get("Groups/{id}")]
        Task<Group> GetByIdAsync([Path] int id);

        [Post("Groups")]
        Task<int> AddAsync([Body] Group item);

        [Delete("Groups")]
        Task DeleteAsync(int id);

        [Get("Groups/GetAllStudents")]
        Task<List<Student>> GetAllStudentsAsync(int groupId);

        [Put("Groups")]
        Task UpdateAsync(Group item);
    }
}

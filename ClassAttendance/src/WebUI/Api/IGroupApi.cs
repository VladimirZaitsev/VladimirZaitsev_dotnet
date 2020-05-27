using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    public interface IGroupApi
    {
        [Post("Groups")]
        Task<int> AddAsync([Body] Group item);

        [Delete("Groups")]
        Task DeleteAsync(int id);

        [Get("Groups")]
        Task<List<Group>> GetAll();

        [Get("Groups/GetAllStudents")]
        Task<IEnumerable<Student>> GetAllStudentsAsync(int groupId);

        [Get("Groups")]
        Task<Group> GetByIdAsync(int id);

        [Get("Groups")]
        Task UpdateAsync(Group item);
    }
}

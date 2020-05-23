using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    public interface IGroupApi
    {
        [Get("Groups/Group")]
        Task<int> AddAsync(Group item);

        [Delete("Groups/Group")]
        Task DeleteAsync(int id);

        [Get("Groups/Groups")]
        Task<List<Group>> GetAll();

        [Get("Groups/GetAllStudents")]
        Task<IEnumerable<Student>> GetAllStudentsAsync(int groupId);

        [Get("Groups/Group")]
        Task<Group> GetByIdAsync(int id);

        [Get("Groups/Group")]
        Task UpdateAsync(Group item);
    }
}

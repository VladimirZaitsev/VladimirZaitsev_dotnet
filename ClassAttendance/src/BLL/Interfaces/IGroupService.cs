using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGroupService : IService<Group>
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync(int groupId);
    }
}

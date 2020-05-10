using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGroupService : IService<Group>
    {
        Task<Group> GetGroupByStudentIdAsync(int studentId);

        Task<IEnumerable<Group>> GetGroupsByLecturerIdAsync(int lecturerId);
    }
}

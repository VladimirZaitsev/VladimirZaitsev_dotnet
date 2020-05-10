using BLL.Models;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGroupService : IService<Group>
    {
        Task<Group> GetGroupByStudentIdAsync(int studentId);
    }
}

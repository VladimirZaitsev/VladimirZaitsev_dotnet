using BLL.Models;

namespace BLL.Interfaces
{
    public interface IGroupService : IService<Group>
    {
        Group GetGroupByStudentId(int studentId);
    }
}

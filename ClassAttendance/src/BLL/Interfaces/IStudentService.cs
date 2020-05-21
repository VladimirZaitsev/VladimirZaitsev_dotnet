using BLL.Models;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IStudentService : IService<Student>
    {
        Task<Group> GetStudentGroupAsync(int studentId);
    }
}

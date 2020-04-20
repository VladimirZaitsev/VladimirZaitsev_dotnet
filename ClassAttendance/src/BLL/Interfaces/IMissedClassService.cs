using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMissedClassService : IService<MissedClass>
    {
        Task<IAsyncEnumerable<MissedClass>> GetMissedLecturesByLecturerAsync(int lecturerId);

        Task<IAsyncEnumerable<MissedClass>> GetMissedLecturesByStudentAsync(int studentId);

        Task<IAsyncEnumerable<Person>> GetSlackersAsync(Class classModel);
    }
}

using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMissedLecturesService : IService<MissedLecture>
    {
        Task<IAsyncEnumerable<MissedLecture>> GetMissedLecturesByLecturerAsync(int lecturerId);

        Task<IAsyncEnumerable<MissedLecture>> GetMissedLecturesByStudentAsync(int studentId);

        Task<IAsyncEnumerable<Person>> GetSlackersAsync(Class classModel);
    }
}

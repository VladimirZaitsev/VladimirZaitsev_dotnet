using DAL.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMissedLecturesService : IService<MissedLectures>
    {
        Task<IAsyncEnumerable<MissedLectures>> GetMissedLecturesByLecturerAsync(int lecturerId);

        Task<IAsyncEnumerable<MissedLectures>> GetMissedLecturesByStudentAsync(int studentId);

        Task<IAsyncEnumerable<Person>> GetSlackersAsync(Class classModel);
    }
}

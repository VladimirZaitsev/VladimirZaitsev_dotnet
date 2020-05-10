using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMissedClassService : IService<MissedClass>
    {
        Task<IEnumerable<MissedClass>> GetMissedLecturesByLecturerAsync(int lecturerId);

        Task<IEnumerable<MissedClass>> GetMissedLecturesByStudentAsync(int studentId);

        Task<IEnumerable<Student>> GetSlackersAsync(Class classModel);
    }
}

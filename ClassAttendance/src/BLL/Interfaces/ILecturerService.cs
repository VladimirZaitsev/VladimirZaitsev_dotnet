using DAL.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ILecturerService
    {
        IAsyncEnumerable<Person> GetLecturers();

        Task<Person> GetLecturer(int lecturerId);

        Task<int> AddLecturerAsync(Person lecturer);

        Task DeleteLecturerAsync(int lecturerId);

        Task UpdateLecturerAsync(Person lecturer);

        Task<IAsyncEnumerable<MissedLectures>> GetMissedLecturesAsync(int lecturerId);
    }
}

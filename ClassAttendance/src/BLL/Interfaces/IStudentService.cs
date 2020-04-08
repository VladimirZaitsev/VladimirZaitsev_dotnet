using DAL.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IStudentService
    {
        IAsyncEnumerable<Person> GetStudents();

        Task<Person> GetStudent(int studentId);

        Task<int> AddStudentAsync(Person student);

        Task DeleteStudentAsync(int studentId);

        Task UpdateStudentAsync(Person student);

        Task<IAsyncEnumerable<Person>> GetSlackersAsync(Class classModel);

        Task<IAsyncEnumerable<MissedLectures>> GetMissedLecturesAsync(int studentId);
    }
}

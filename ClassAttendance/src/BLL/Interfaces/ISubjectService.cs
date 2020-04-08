using DAL.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISubjectService
    {
        IAsyncEnumerable<Subject> GetSubjectsAsync();

        Task<Subject> GetSubjectAsync(int subjectId);

        Task<int> AddSubjectAsync(Subject student);

        Task DeleteSubjectAsync(int subjectId);

        Task UpdatedSubjectAsync(Subject student);

        Task<IAsyncEnumerable<Person>> GetLecturersAsync(int subjectId);

        Task<IAsyncEnumerable<Person>> GetStudentsAsync(int subjectId);
    }
}

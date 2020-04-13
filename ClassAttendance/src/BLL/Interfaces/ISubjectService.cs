using BLL.Models;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ISubjectService : IService<Subject>
    {
        IAsyncEnumerable<Person> GetLecturersAsync(int subjectId);

        IAsyncEnumerable<Person> GetStudentsAsync(int subjectId);
    }
}

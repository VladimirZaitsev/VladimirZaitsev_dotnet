using BLL.Models;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ISubjectService : IService<Subject>
    {
        IEnumerable<Person> GetLecturersAsync(int subjectId);

        IEnumerable<Person> GetStudentsAsync(int subjectId);
    }
}

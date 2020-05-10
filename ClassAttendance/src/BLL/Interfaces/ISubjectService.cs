using BLL.Models;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ISubjectService : IService<Subject>
    {
        IEnumerable<Lecturer> GetLecturersAsync(int subjectId);

        IEnumerable<Student> GetStudentsAsync(int subjectId);
    }
}

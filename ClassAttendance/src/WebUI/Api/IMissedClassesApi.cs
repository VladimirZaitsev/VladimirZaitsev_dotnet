using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    [AllowAnyStatusCode]
    public interface IMissedClassesApi
    {
        [Get("MissedClasses")]
        Task<List<MissedClass>> GetAll();

        [Get("MissedClasses/{id}")]
        Task<MissedClass> GetByIdAsync([Path] int id);

        [Post("MissedClasses")]
        Task<int> AddAsync([Body] MissedClass missedClass);

        [Put("MissedClasses")]
        Task UpdateAsync([Body] MissedClass missedClass);

        [Delete("MissedClasses")]
        Task DeleteAsync(int id);

        [Get("MissedClasses/Student/{id}")]
        Task<List<MissedClass>> GetStudentMissedClassesAsync([Path] int id);

        [Get("MissedClasses/Lecturer/{id}")]
        Task<List<MissedClass>> GetLecturerMissedClassesAsync([Path] int id);

        [Get("MissedClasses/Slackers")]
        Task<List<Student>> GetSlackersAsync([Body] Class classModel);
    }
}

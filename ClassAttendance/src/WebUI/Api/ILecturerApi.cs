using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    [AllowAnyStatusCode]
    public interface ILecturerApi
    {
        [Get("Lecturers")]
        Task<List<Lecturer>> GetAll();

        [Get("Lecturers/{id}")]
        Task<Lecturer> GetByIdAsync([Path] int id);

        [Post("Lecturers")]
        Task<int> AddAsync([Body] Lecturer lecturer);

        [Put("Lecturers")]
        Task UpdateAsync([Body] Lecturer lecturer);

        [Delete("Lecturers")]
        Task DeleteAsync(int id);
    }
}

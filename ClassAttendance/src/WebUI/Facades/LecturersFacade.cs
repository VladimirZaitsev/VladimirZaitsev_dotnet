using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Api;

namespace WebUI.Facades
{
    public class LecturersFacade
    {
        private readonly ILecturerApi _lecturerApi;

        public LecturersFacade(ILecturerApi lecturerApi)
        {
            _lecturerApi = lecturerApi;
        }

        public async Task<IEnumerable<Lecturer>> GetLecturers() => await _lecturerApi.GetAll();

        public async Task AddLecturerAsync(Lecturer lecturer) => await _lecturerApi.AddAsync(lecturer);

        public async Task UpdateLecturerAsync(Lecturer lecturer) => await _lecturerApi.UpdateAsync(lecturer);

        public async Task DeleteLecturerAsync(int lecturerId) => await _lecturerApi.DeleteAsync(lecturerId);

        public async Task<Lecturer> GetByIdAsync(int id) => await _lecturerApi.GetByIdAsync(id);
    }
}

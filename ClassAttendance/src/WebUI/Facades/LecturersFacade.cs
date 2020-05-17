using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Facades
{
    public class LecturersFacade
    {
        private readonly IService<Lecturer> _lecturerService;

        public LecturersFacade(IService<Lecturer> lecturerService)
        {
            _lecturerService = lecturerService;
        }

        public IEnumerable<Lecturer> GetLecturers() => _lecturerService.GetAll();

        public async Task AddLecturerAsync(Lecturer lecturer) => await _lecturerService.AddAsync(lecturer);

        public async Task UpdateLecturerAsync(Lecturer lecturer) => await _lecturerService.UpdateAsync(lecturer);

        public async Task DeleteLecturerAsync(int lecturerId) => await _lecturerService.DeleteAsync(lecturerId);

        public async Task<Lecturer> GetByIdAsync(int id) => await _lecturerService.GetByIdAsync(id);
    }
}

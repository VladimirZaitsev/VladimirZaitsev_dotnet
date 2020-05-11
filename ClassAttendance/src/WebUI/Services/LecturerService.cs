using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Services
{
    public class LecturerService
    {
        private readonly IService<Lecturer> _lecturerService;

        public LecturerService(IService<Lecturer> lecturerService)
        {
            _lecturerService = lecturerService;
        }

        public IEnumerable<Lecturer> GetLecturers() => _lecturerService.GetAll();

        public async Task AddLecturerAsync(Lecturer lecturer)
        {
            await _lecturerService.AddAsync(lecturer);
        }

        public async Task UpdateLecturerAsync(Lecturer lecturer)
        {
            await _lecturerService.UpdateAsync(lecturer);
        }

        public async Task DeleteLecturerAsync(int lecturerId)
        {
            await _lecturerService.DeleteAsync(lecturerId);
        }
    }
}

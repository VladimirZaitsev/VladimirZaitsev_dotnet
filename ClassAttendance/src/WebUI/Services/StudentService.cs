using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models.ViewModels;

namespace WebUI.Services
{
    public class StudentService
    {
        private readonly IStudentService _studentService;

        public StudentService(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IEnumerable<StudentViewModel>> GetStudentListAsync()
        {
            var students = _studentService.GetAll();
            var viewModels = students.Select(async student => new StudentViewModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Address = student.Address,
                GroupName = await GetGroupNameAsync(student.Id),
            });

            var result = await Task.WhenAll(viewModels);

            return result;
        }

        private async Task<string> GetGroupNameAsync(int studentId)
        {
            var group = await _studentService.GetStudentGroupAsync(studentId);

            return group.Name;
        }
    }
}
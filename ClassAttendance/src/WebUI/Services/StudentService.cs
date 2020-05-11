using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Models.ViewModels.StudentModel;

namespace WebUI.Services
{
    public class StudentService
    {
        private readonly IStudentService _studentService;
        private readonly IService<Group> _groupService;

        public StudentService(IStudentService studentService, IService<Group> groupService)
        {
            _studentService = studentService;
            _groupService = groupService;
        }

        public async Task<IEnumerable<StudentViewModel>> GetStudentListAsync()
        {
            var students = _studentService.GetAll();

            // Unable to do this using Select because i'm getting error of running second operation
            // Before first was completed
            var list = new List<StudentViewModel>();
            foreach (var student in students)
            {
                list.Add(new StudentViewModel
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Address = student.Address,
                    GroupName = await GetGroupNameAsync(student.GroupId),
                });
            }

            return list;
        }

        public StudentManageViewModel GetViewModel()
        {
            var model = new StudentManageViewModel
            {
                Groups = _groupService.GetAll(),
                Student = new Student(),
            };

            return model;
        }

        public async Task<StudentManageViewModel> GetViewModel(int studentId)
        {
            var model = new StudentManageViewModel
            {
                Groups = _groupService.GetAll(),
                Student = await _studentService.GetByIdAsync(studentId),
            };

            return model;
        }

        public async Task AddStudentAsync(Student student)
        {
            await _studentService.AddAsync(student);
        }

        public async Task EditStudentAsync(Student student)
        {
            await _studentService.UpdateAsync(student);
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            await _studentService.DeleteAsync(studentId);
        }

        private async Task<string> GetGroupNameAsync(int groupId)
        {
            var group = await _studentService.GetStudentGroupAsync(groupId);

            return group.Name;
        }
    }
}
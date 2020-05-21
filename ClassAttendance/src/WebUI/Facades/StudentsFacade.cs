using BLL.Interfaces;
using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebUI.Api;
using WebUI.Models.ViewModels.StudentModel;

namespace WebUI.Facades
{
    public class StudentsFacade
    {
        private const string ClientName = "StudentApi";
        private readonly IStudentApi _studentApi;
        private readonly IService<Group> _groupService;

        public StudentsFacade(IHttpClientFactory httpClientFactory, IService<Group> groupService)
        {
            if (httpClientFactory is null)
            {
                throw new System.ArgumentNullException(nameof(httpClientFactory));
            }

            var client = httpClientFactory.CreateClient(ClientName);
            _studentApi = new RestClient(client).For<IStudentApi>();
            _groupService = groupService;
        }

        public async Task<IEnumerable<StudentViewModel>> GetStudentListAsync()
        {
            var students = await _studentApi.GetStudents();

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
                    GroupName = await GetGroupNameAsync(student.Id),
                });
            }

            return list;
        }

        public StudentManageViewModel GetViewModel()
        {
            var model = new StudentManageViewModel
            {
                Groups = _groupService.GetAll().ToList(),
                Student = new Student(),
            };

            return model;
        }

        public async Task<StudentManageViewModel> GetViewModel(int studentId)
        {
            var model = new StudentManageViewModel
            {
                Groups = _groupService.GetAll().ToList(),
                Student = await _studentApi.GetStudentAsync(studentId),
            };

            return model;
        }

        public async Task AddStudentAsync(Student student)
        {
            await _studentApi.AddStudentAsync(student);
        }

        public async Task EditStudentAsync(Student student)
        {
            await _studentApi.UpdateStudentAsync(student);
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            await _studentApi.DeleteStudentAsync(studentId);
        }

        private async Task<string> GetGroupNameAsync(int studentId)
        {
            var group = await _studentApi.GetStudentGroupAsync(studentId);

            return group.Name;
        }
    }
}
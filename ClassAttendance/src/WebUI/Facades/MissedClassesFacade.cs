using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Api;
using WebUI.Models.ViewModels.ClassModel;
using WebUI.Models.ViewModels.MissedClass;

namespace WebUI.Facades
{
    public class MissedClassesFacade
    {
        private readonly IMissedClassesApi _missedClassesApi;
        private readonly IStudentApi _studentApi;
        private readonly IClassesApi _classesApi;
        private readonly ISubjectService _subjectService;

        public MissedClassesFacade(
            IMissedClassesApi missedClassesApi,
            IStudentApi studentApi,
            IClassesApi classesApi,
            ISubjectService subjectService)
        {
            _missedClassesApi = missedClassesApi;
            _studentApi = studentApi;
            _classesApi = classesApi;
            _subjectService = subjectService;
        }

        public async Task<IEnumerable<MissedClassViewModel>> GetMissedLecturesAsync()
        {
            var missedClasses = await _missedClassesApi.GetAll();
            var viewModels = await GetMissedClassViewModelsAsync(missedClasses);

            return viewModels;
        }

        public async Task<MissedClassManageViewModel> GetManageViewModelAsync()
        {
            var classDisplayModels = await GetDisplayModelsAsync();

            var students = await _studentApi.GetStudents();

            var viewModel = new MissedClassManageViewModel
            {
                Classes = classDisplayModels.ToList(),
                Students = students,
            };

            return viewModel;
        }

        public async Task<MissedClassManageViewModel> GetManageViewModelAsync(int id)
        {
            var missedClass = await _missedClassesApi.GetByIdAsync(id);
            var classDisplayModels = await GetDisplayModelsAsync();
            var students = await _studentApi.GetStudents();

            var viewModel = new MissedClassManageViewModel
            {
                ClassId = missedClass.ClassId,
                StudentId = missedClass.StudentId,
                Id = missedClass.Id,
                Classes = classDisplayModels.ToList(),
                Students = students,
            };

            return viewModel;
        }

        public async Task AddAsync(MissedClass model) => await _missedClassesApi.AddAsync(model);

        public async Task UpdateAsync(MissedClass model) => await _missedClassesApi.UpdateAsync(model);

        public async Task DeleteAsync(int id) => await _missedClassesApi.DeleteAsync(id);

        public async Task<IEnumerable<MissedClassViewModel>> GetStudentMissedClassesAsync(int studentId)
        {
            var missedClasses = await _missedClassesApi.GetStudentMissedClassesAsync(studentId);
            var viewModels = await GetMissedClassViewModelsAsync(missedClasses);

            return viewModels;
        }

        public async Task<IEnumerable<MissedClassViewModel>> GetLecturerMissedClassesAsync(int studentId)
        {
            var missedClasses = await _missedClassesApi.GetLecturerMissedClassesAsync(studentId);
            var viewModels = await GetMissedClassViewModelsAsync(missedClasses);

            return viewModels;
        }

        private async Task<IEnumerable<ClassDisplayModel>> GetDisplayModelsAsync()
        {
            var classes = await _classesApi.GetClasses();
            var displayModels = new List<ClassDisplayModel>();

            foreach (var cls in classes)
            {
                var subject = await _subjectService.GetByIdAsync(cls.SubjectId);
                displayModels.Add(new ClassDisplayModel
                {
                    Id = cls.Id,
                    DisplayName = subject.Name + " " + cls.StartDate.ToString("g"),
                });
            }

            return displayModels;
        }

        private async Task<List<MissedClassViewModel>> GetMissedClassViewModelsAsync(IEnumerable<MissedClass> missedClasses)
        {
            var viewModels = new List<MissedClassViewModel>();
            foreach (var missedClass in missedClasses)
            {
                var group = await _studentApi.GetStudentGroupAsync(missedClass.StudentId);
                var student = await _studentApi.GetStudentAsync(missedClass.StudentId);
                var cls = await _classesApi.GetClassAsync(missedClass.ClassId);
                var subject = await _subjectService.GetByIdAsync(cls.SubjectId);


                viewModels.Add(new MissedClassViewModel
                {
                    Id = missedClass.Id,
                    ClassId = missedClass.ClassId,
                    Date = cls.StartDate,
                    GroupName = group.Name,
                    StudentId = missedClass.StudentId,
                    StudentName = student.FirstName + " " + student.LastName,
                    SubjectName = subject.Name,
                });
            }

            return viewModels;
        }
    }
}

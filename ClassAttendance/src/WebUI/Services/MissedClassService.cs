using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models.ViewModels.ClassModel;
using WebUI.Models.ViewModels.MissedClass;

namespace WebUI.Services
{
    public class MissedClassService
    {
        private readonly IMissedClassService _missedClassService;
        private readonly IStudentService _studentService;
        private readonly ISubjectService _subjectService;
        private readonly IService<Class> _classService;

        public MissedClassService(IMissedClassService missedClassService,
            IStudentService studentService,
            ISubjectService subjectService,
            IService<Class> classService)
        {
            _missedClassService = missedClassService;
            _studentService = studentService;
            _subjectService = subjectService;
            _classService = classService;
        }

        public async Task<IEnumerable<MissedClassViewModel>> GetMissedLecturesAsync()
        {
            var missedClasses = _missedClassService.GetAll();
            var viewModels = new List<MissedClassViewModel>();
            foreach (var missedClass in missedClasses)
            {
                var group = await _studentService.GetStudentGroupAsync(missedClass.StudentId);
                var student = await _studentService.GetByIdAsync(missedClass.StudentId);
                var cls = await _classService.GetByIdAsync(missedClass.ClassId);
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

        public async Task<MissedClassManageViewModel> GetManageViewModelAsync()
        {
            var classDisplayModels = await GetDisplayModelsAsync();

            var students = _studentService.GetAll().ToList();

            var viewModel = new MissedClassManageViewModel
            {
                Classes = classDisplayModels.ToList(),
                Students = students,
            };

            return viewModel;
        }

        public async Task<MissedClassManageViewModel> GetManageViewModelAsync(int id)
        {
            var missedClass = await _missedClassService.GetByIdAsync(id);
            var classDisplayModels = await GetDisplayModelsAsync();
            var students = _studentService.GetAll().ToList();

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

        public async Task AddAsync(MissedClass model) => await _missedClassService.AddAsync(model);

        public async Task UpdateAsync(MissedClass model) => await _missedClassService.UpdateAsync(model);

        public async Task DeleteAsync(int id) => await _missedClassService.DeleteAsync(id);

        private async Task<IEnumerable<ClassDisplayModel>> GetDisplayModelsAsync()
        {
            var classes = _classService.GetAll();
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
    }
}

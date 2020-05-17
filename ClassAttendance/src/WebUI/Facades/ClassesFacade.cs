using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models.ViewModels.ClassModel;

namespace WebUI.Facades
{
    public class ClassesFacade
    {
        private readonly IService<Class> _classService;
        private readonly IGroupService _groupService;
        private readonly IService<Lecturer> _lecturerService;
        private readonly ISubjectService _subjectService;

        public ClassesFacade(IService<Class> classService, IGroupService groupService, IService<Lecturer> lecturerService, ISubjectService subjectService)
        {
            _classService = classService;
            _groupService = groupService;
            _lecturerService = lecturerService;
            _subjectService = subjectService;
        }

        public async Task<IEnumerable<ClassViewModel>> GetClassViewModelsAsync()
        {
            var classes = _classService.GetAll();
            var viewModels = new List<ClassViewModel>();

            foreach (var item in classes)
            {
                viewModels.Add(await GetClassViewModelAsync(item.Id));
            }

            return viewModels;
        }

        public async Task AddClassAsync(Class item, List<string> groupNames)
        {
            var groupIds = from grp in _groupService.GetAll().ToList()
                           join name in groupNames on grp.Name equals name
                           select grp.Id;

            item.GroupIds = groupIds.ToList();

            await _classService.AddAsync(item);
        }

        public async Task UpdateClassAsync(Class item, List<string> groupNames)
        {
            var groupIds = from grp in _groupService.GetAll().ToList()
                           join name in groupNames on grp.Name equals name
                           select grp.Id;

            item.GroupIds = groupIds.ToList();

            await _classService.UpdateAsync(item);
        }

        public async Task DeleteClassAsync(int id) => await _classService.DeleteAsync(id);

        public async Task<Class> GetByIdAsync(int id) => await _classService.GetByIdAsync(id);

        public ClassManageViewModel GetClassManageViewModel()
        {
            var lecturers = _lecturerService.GetAll().ToList();
            var subjects = _subjectService.GetAll().ToList();
            var groups = _groupService.GetAll().ToList();

            var viewModel = new ClassManageViewModel
            {
                Subjects = subjects,
                Lecturers = lecturers,
                Groups = groups,
            };

            return viewModel;
        }

        public async Task<ClassManageViewModel> GetClassManageViewModel(int id)
        {
            var item = await _classService.GetByIdAsync(id);
            var lecturers = _lecturerService.GetAll().ToList();
            var subjects = _subjectService.GetAll().ToList();
            var groups = _groupService.GetAll().ToList();

            var viewModel = new ClassManageViewModel
            {
                CabinetId = item.CabinetId,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                Id = item.Id,
                LecturerId = item.LecturerId,
                SubjectId = item.LecturerId,
                Subjects = subjects,
                Lecturers = lecturers,
                Groups = groups,
                SelectedGroups = groups.Where(group => item.GroupIds.Contains(group.Id)).ToList(),
            };

            return viewModel;
        }

        private async Task<ClassViewModel> GetClassViewModelAsync(int id)
        {
            var item = await GetByIdAsync(id);
            var lecturer = await _lecturerService.GetByIdAsync(item.LecturerId);
            var groupNames = new List<string>();
            foreach (var groupId in item.GroupIds)
            {
                groupNames.Add(await GetGroupName(groupId));
            }

            var viewModel = new ClassViewModel
            {
                Class = item,
                GroupNames = groupNames,
                LecturerName = lecturer.FirstName + " " + lecturer.LastName,
                SubjectName = await GetSubjectName(item.SubjectId),
            };

            return viewModel;
        }

        private async Task<string> GetGroupName(int id)
        {
            var group = await _groupService.GetByIdAsync(id);

            return group.Name;
        }

        private async Task<string> GetSubjectName(int id)
        {
            var subject = await _subjectService.GetByIdAsync(id);

            return subject.Name;
        }
    }
}

using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Api;
using WebUI.Models.ViewModels.ClassModel;

namespace WebUI.Facades
{
    public class ClassesFacade
    {
        private readonly IClassesApi _classesApi;
        private readonly IGroupApi _groupApi;
        private readonly ILecturerApi _lecturerApi;
        private readonly ISubjectApi _subjectApi;

        public ClassesFacade(IClassesApi classesApi, IGroupApi groupApi, ILecturerApi lecturerApi, ISubjectApi subjectApi)
        {
            _classesApi = classesApi;
            _groupApi = groupApi;
            _lecturerApi = lecturerApi;
            _subjectApi = subjectApi;
        }

        public async Task<IEnumerable<ClassViewModel>> GetClassViewModelsAsync()
        {
            var classes = await _classesApi.GetAll();
            var viewModels = new List<ClassViewModel>();

            foreach (var item in classes)
            {
                viewModels.Add(await GetClassViewModelAsync(item.Id));
            }

            return viewModels;
        }

        public async Task AddClassAsync(Class item, List<string> groupNames)
        {
            var groups = await _groupApi.GetAll();
            var groupIds = from grp in groups
                           join name in groupNames on grp.Name equals name
                           select grp.Id;

            item.GroupIds = groupIds.ToList();

            await _classesApi.AddAsync(item);
        }

        public async Task UpdateClassAsync(Class item, List<string> groupNames)
        {
            var groupIds = from grp in await _groupApi.GetAll()
                           join name in groupNames on grp.Name equals name
                           select grp.Id;

            item.GroupIds = groupIds.ToList();

            await _classesApi.UpdateAsync(item);
        }

        public async Task DeleteClassAsync(int id) => await _classesApi.DeleteAsync(id);

        public async Task<Class> GetByIdAsync(int id) => await _classesApi.GetByIdAsync(id);

        public async Task<ClassManageViewModel> GetClassManageViewModel()
        {
            var lecturers = await _lecturerApi.GetAll();
            var subjects = await _subjectApi.GetAll();
            var groups = await _groupApi.GetAll();

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
            var item = await _classesApi.GetByIdAsync(id);
            var lecturers = await _lecturerApi.GetAll();
            var subjects = await _subjectApi.GetAll();
            var groups = await _groupApi.GetAll();

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
            var lecturer = await _lecturerApi.GetByIdAsync(item.LecturerId);
            var groupNames = new List<string>();
            foreach (var groupId in item.GroupIds)
            {
                groupNames.Add(await GetGroupName(groupId));
            }

            var viewModel = new ClassViewModel
            {
                Class = item,
                GroupNames = groupNames,
                LecturerName = $"{lecturer.FirstName} {lecturer.LastName}",
                SubjectName = await GetSubjectName(item.SubjectId),
            };

            return viewModel;
        }

        private async Task<string> GetGroupName(int id)
        {
            var group = await _groupApi.GetByIdAsync(id);

            return group.Name;
        }

        private async Task<string> GetSubjectName(int id)
        {
            var subject = await _subjectApi.GetByIdAsync(id);

            return subject.Name;
        }
    }
}

using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models.ViewModels.GroupModel;

namespace WebUI.Services
{
    public class GroupService
    {
        private readonly IGroupService _groupService;

        public GroupService(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public async Task<IEnumerable<GroupViewModel>> GetGroupViewModelsAsync()
        {
            var groups = _groupService.GetAll();
            var viewModels = new List<GroupViewModel>();

            foreach (var group in groups)
            {
                viewModels.Add(await GetGroupViewModelAsync(group.Id));
            }

            return viewModels;
        }

        public async Task AddGroupAsync(Group subject) => await _groupService.AddAsync(subject);

        public async Task UpdateGroupAsync(Group subject) => await _groupService.UpdateAsync(subject);

        public async Task DeleteGroupAsync(int id) => await _groupService.DeleteAsync(id);

        public async Task<Group> GetByIdAsync(int id) => await _groupService.GetByIdAsync(id);

        private async Task<GroupViewModel> GetGroupViewModelAsync(int id)
        {
            var group = await GetByIdAsync(id);
            var students = await _groupService.GetAllStudentsAsync(id);

            var viewModel = new GroupViewModel
            {
                Group = group,
                Students = students.ToList(),
            };

            return viewModel;
        }
    }
}

using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Api;
using WebUI.Models.ViewModels.GroupModel;

namespace WebUI.Facades
{
    public class GroupsFacade
    {
        private readonly IGroupApi _groupApi;

        public GroupsFacade(IGroupApi groupApi)
        {
            _groupApi = groupApi;
        }

        public async Task<IEnumerable<GroupViewModel>> GetGroupViewModelsAsync()
        {
            var groups = await _groupApi.GetAll();
            var viewModels = new List<GroupViewModel>();

            foreach (var group in groups)
            {
                viewModels.Add(await GetGroupViewModelAsync(group.Id));
            }

            return viewModels;
        }

        public async Task AddGroupAsync(Group subject) => await _groupApi.AddAsync(subject);

        public async Task UpdateGroupAsync(Group subject) => await _groupApi.UpdateAsync(subject);

        public async Task DeleteGroupAsync(int id) => await _groupApi.DeleteAsync(id);

        public async Task<Group> GetByIdAsync(int id) => await _groupApi.GetByIdAsync(id);

        private async Task<GroupViewModel> GetGroupViewModelAsync(int id)
        {
            var group = await GetByIdAsync(id);
            var students = await _groupApi.GetAllStudentsAsync(id);

            var viewModel = new GroupViewModel
            {
                Group = group,
                Students = students,
            };

            return viewModel;
        }
    }
}

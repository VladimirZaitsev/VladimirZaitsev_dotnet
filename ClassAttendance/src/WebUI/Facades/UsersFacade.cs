using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Facades
{
    public class UsersFacade
    {
        private readonly IIdentityService _identityService;

        public UsersFacade(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<User> GetUserAsync(string id) => await _identityService.GetByIdAsync(id);

        public async Task<List<User>> GetUsersAsync() => await _identityService.GetUsersAsync();

        public async Task UpdateUserAsync(User user) => await _identityService.UpdateAsync(user);

        public async Task DeleteUserAsync(string id) => await _identityService.DeleteAsync(id);
    }
}

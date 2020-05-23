using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Facades
{
    public class UsersFacade
    {
        private readonly IIdentityService _userService;

        public UsersFacade(IIdentityService userService)
        {
            _userService = userService;
        }

        public async Task<User> GetUserAsync(string id) => await _userService.GetByIdAsync(id);

        public async Task<List<User>> GetUsersAsync() => await _userService.GetUsersAsync();

        public async Task UpdateUserAsync(User user) => await _userService.UpdateAsync(user);

        public async Task DeleteUserAsync(string id) => await _userService.DeleteAsync(id);
    }
}

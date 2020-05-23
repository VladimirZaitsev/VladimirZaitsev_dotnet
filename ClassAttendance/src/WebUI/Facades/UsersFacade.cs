using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Api;

namespace WebUI.Facades
{
    public class UsersFacade
    {
        private readonly IUserApi _userApi;

        public UsersFacade(IUserApi userService)
        {
            _userApi = userService;
        }

        public async Task<User> GetUserAsync(string id) => await _userApi.GetByIdAsync(id);

        public async Task<List<User>> GetUsersAsync() => await _userApi.GetUsersAsync();

        public async Task UpdateUserAsync(User user) => await _userApi.UpdateAsync(user);

        public async Task DeleteUserAsync(string id) => await _userApi.DeleteAsync(id);
    }
}

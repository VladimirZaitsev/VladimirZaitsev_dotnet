using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models.Account;

namespace WebUI.Facades
{
    public class UsersFacade
    {
        private readonly UserManager<User> _userManager;

        public UsersFacade(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetUserAsync(string id) => await _userManager.FindByIdAsync(id);

        public async Task<List<User>> GetUsersAsync() => await _userManager.Users.ToListAsync();

        public async Task UpdateUserAsync(User user) => await _userManager.UpdateAsync(user);

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
        }
    }
}

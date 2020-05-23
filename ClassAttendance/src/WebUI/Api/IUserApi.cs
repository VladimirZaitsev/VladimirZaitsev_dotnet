using BLL.Models;
using Microsoft.AspNetCore.Identity;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    public interface IUserApi
    {
        [Get("Identity/User")]
        Task<User> GetByIdAsync(string id);

        [Get("Identity/Users")]
        Task<List<User>> GetUsersAsync();

        [Put("Identity/User")]
        Task UpdateAsync(User user);

        [Delete("Identity/User")]
        Task DeleteAsync(string id);

        [Post("Identity/User")]
        Task RegisterAsync(User user, string password);

        [Post("Identity/SignOut")]
        Task SignOutAsync();

        [Post("Identity/SignIn")]
        Task SignInAsync(string email, string password);

        [Get("Identity/User")]
        Task<User> FindByEmailAsync(string email);

        [Post("Identity/Role")]
        Task CreateRoleAsync(IdentityRole role);

        [Get("Identity/RoleExists")]
        Task<bool> RoleExistsAsync(string roleName);

        [Post("Identity/AddToRole")]
        Task AddToRoleAsync(User user, string roleName);
    }
}

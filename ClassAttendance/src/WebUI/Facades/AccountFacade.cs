using BLL.Models;
using System.Threading.Tasks;
using WebUI.Api;
using WebUI.Identity;
using WebUI.Models.Account;

namespace WebUI.Facades
{
    public class AccountFacade
    {
        private readonly IUserApi _userApi;

        public AccountFacade(IUserApi userApi)
        {
            _userApi = userApi;
        }

        public async Task RegisterAsync(RegisterModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = $"{model.FirstName}{model.LastName}",
            };

            await _userApi.RegisterAsync(user, model.Password);
            await _userApi.AddToRoleAsync(user, Roles.User);
        }

        public async Task SignInAsync(LoginModel model) => await _userApi.SignInAsync(model.Email, model.Password);

        public async Task SignOutAsync() => await _userApi.SignOutAsync();
    }
}

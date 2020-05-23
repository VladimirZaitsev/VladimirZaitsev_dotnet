using BLL.Interfaces;
using BLL.Models;
using System.Threading.Tasks;
using WebUI.Identity;
using WebUI.Models.Account;

namespace WebUI.Facades
{
    public class AccountFacade
    {
        private readonly IIdentityService _identityService;

        public AccountFacade(IIdentityService identityService)
        {
            _identityService = identityService;
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

            await _identityService.RegisterAsync(user, model.Password);
            await _identityService.AddToRoleAsync(user, Roles.User);
        }

        public async Task SignInAsync(LoginModel model) => await _identityService.SignInAsync(model.Email, model.Password);

        public async Task SignOutAsync() => await _identityService.SignOutAsync();
    }
}

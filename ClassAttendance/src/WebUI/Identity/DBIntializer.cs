using BLL.Interfaces;
using BLL.Models;
using DAL.Core;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace WebUI.Identity
{
    public class DBIntializer
    {
        private readonly IIdentityService _identityService;

        public DBIntializer(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task Seed()
        {
            var password = "Password123+";

            var users = new User[]
            {
                new User
                {
                    UserName = "EvgeniyaLepshaya",
                    FirstName = "Evgeniya",
                    LastName = "Lepshaya",
                    Email = "user@gmail.com",
                },

                new User
                {
                    UserName = "MaximLopatsin",
                    FirstName = "Maxim",
                    LastName = "Lopatsin",
                    Email = "manager@gmail.com",
                },
            };

            var roles = new string[]
            {
                Roles.User,
                Roles.Manager,
            };

            foreach (var role in roles)
            {
                if (!await _identityService.RoleExistsAsync(role))
                {
                    await _identityService.CreateRoleAsync(new IdentityRole(role));
                }
            }

            var i = 0;
            foreach (var user in users)
            {
                if (await _identityService.FindByEmailAsync(user.Email) == null)
                {
                    await _identityService.RegisterAsync(user, password);
                    await _identityService.AddToRoleAsync(user, roles[i++]);
                }
            }
        }
    }
}

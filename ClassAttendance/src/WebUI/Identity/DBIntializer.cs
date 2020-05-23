using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;
using WebUI.Models.Account;

namespace WebUI.Identity
{
    public static class DBIntializer
    {
        public static async Task Seed(UserContext context)
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

            var userManager = context.GetService<UserManager<User>>();
            var roleManager = context.GetService<RoleManager<IdentityRole>>();
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var i = 0;
            foreach (var user in users)
            {
                if (await userManager.FindByEmailAsync(user.Email) == null)
                {
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, roles[i++]);
                }
            }
        }
    }
}

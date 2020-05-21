using Microsoft.AspNetCore.Identity;

namespace WebUI.Models.Account
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}

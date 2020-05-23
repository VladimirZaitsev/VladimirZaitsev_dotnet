using Microsoft.AspNetCore.Identity;

namespace DAL.Dtos
{
    public class UserDto : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}

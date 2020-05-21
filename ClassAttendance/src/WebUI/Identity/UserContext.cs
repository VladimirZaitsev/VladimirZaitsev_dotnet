﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebUI.Models.Account;

namespace WebUI.Identity
{
    public class UserContext : IdentityDbContext<User>
    {
        public UserContext(DbContextOptions<UserContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

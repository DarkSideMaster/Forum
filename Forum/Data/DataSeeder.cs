﻿using Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Forum.Data
{
    public class DataSeeder
    {
        private ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedSuperUser()
        {
            var roleStore = new RoleStore<IdentityRole>(_context);
            var userStore = new UserStore<ApplicationUser>(_context);

            var user = new ApplicationUser
            {
                UserName = "ForumAdmin",
                NormalizedUserName = "forumadmin",
                Email = "darkpanter@ukr.net",
                NormalizedEmail = "darkpanter@ukr.net",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };


            var hasher = new PasswordHasher<ApplicationUser>();
            var hasshePassword = hasher.HashPassword(user, "admin");
            user.PasswordHash = hasshePassword;

            var hasAmdinRole = _context.Roles.Any(roles => roles.Name == "Admin");

            if (!hasAmdinRole)
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin"
                });
            }

            var hasSuperUser = _context.Users.Any(u => u.NormalizedUserName == user.UserName);

            if (!hasSuperUser)
            {
                userStore.CreateAsync(user);
                userStore.AddToRoleAsync(user, "Admin");
            }

            _context.SaveChanges();
        }
    }
}

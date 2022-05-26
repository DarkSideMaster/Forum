using Forum.Models;
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

        public async Task SeedSuperUser() 
        {
            var roleStore = new RoleStore<IdentityRole>(_context);
            var userStore = new UserStore<ApplicationUser>(_context);

            var user = new ApplicationUser
            {
                UserName ="ForumAdmin",
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

            var hasAmdinRole = _context.Roles.Any(r => r.Name == "Admin");

            if (!hasAmdinRole)
            {
               await roleStore.CreateAsync(new IdentityRole 
               { Name = "Admin", 
                NormalizedName = "admin" 
               });
            }

            var hasSuperUser = _context.Users.Any(u => u.NormalizedUserName == user.UserName);
          
            if (hasSuperUser)
            {
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "Admin");
            }

            await _context.SaveChangesAsync();
        }
    }
}

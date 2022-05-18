using Forum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { set; get; }
        public DbSet<Forums> Forums { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<PostReply> PostReplys { set; get; }

    }
}
using Forum.Models;

namespace Forum.ForumHelpers
{
    public class Roles
    {
        public List<CurrentRole> GetAllRoles()
        {
            return new List<CurrentRole>
            {
                new CurrentRole() { Id = 1, RoleName = "Адміністратор",      Description = "Forum Administrator"},
                new CurrentRole() { Id = 2, RoleName = "Головний модератор", Description = "Super Moderator"},
                new CurrentRole() { Id = 3, RoleName = "Модератор",          Description = "Forum moderator"}
            };
        }
    }
}

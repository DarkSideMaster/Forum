using Forum.Models;

namespace Forum.ForumHelpers
{
    public class Roles
    {
        public List<CurrentRoleModel> GetAllRoles()
        {
            return new List<CurrentRoleModel>
            {
                new CurrentRoleModel() { Id = 1, RoleName = "Адміністратор",      Description = "Forum Administrator"},
                new CurrentRoleModel() { Id = 2, RoleName = "Головний модератор", Description = "Super Moderator"},
                new CurrentRoleModel() { Id = 3, RoleName = "Модератор",          Description = "Forum moderator"}
            };
        }
    }
}

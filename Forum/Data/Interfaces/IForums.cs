using Forum.Models;

namespace Forum.Data
{
    public interface IForums
    {
        Forums GetbyId(int Id);
        IEnumerable<Forums> GetAll();

        Task Create(Forums forum);
        Task Delete(int forumId);
        Task UpdateForumDescription(int forumId, string newDescription);
        Task UpdateForumTitle(int forumId, string newTitle);
        IEnumerable<ApplicationUser> GetActiveUsers(int id);
        bool HasRecentPost(int id);
    }
}

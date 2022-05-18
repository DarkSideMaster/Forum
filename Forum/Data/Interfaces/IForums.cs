using Forum.Models;

namespace Forum.Data
{
    public interface IForums
    {
        Forums GetbyId(int Id);
        IEnumerable<Forums> GetAll();
        IEnumerable<ApplicationUser> GetAllActiveUsers();

        Task Create(Forums forum);
        Task Delete(int forumId);
        Task UpdateForumDescription(int forumId, string newDescription);  

    }
}

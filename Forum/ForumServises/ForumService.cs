using Forum.Data;
using Forum.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.Services
{
    public class ForumService : IForums
    {

        private readonly ApplicationDbContext _context;

        public ForumService(ApplicationDbContext context)
        {
            _context = context;

        }

        public Task Create(Forums forum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int forumId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Forums> GetAll()
        {
            return _context.Forums.Include(forum => forum.Posts);
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        public Forums GetbyId(int Id)
        {
            var forum = _context.Forums.Where(x => x.Id == Id)
                .Include(f => f.Posts).ThenInclude(u => u.User)
                .Include(f => f.Posts).ThenInclude(u => u.Replies).ThenInclude(r => r.User)
                .FirstOrDefault();

            return forum;
        }

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }
    }
}
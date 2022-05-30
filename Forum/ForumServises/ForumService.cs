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

        public async Task Create(Forums forum)
        {
            _context.Forums.Add(forum);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int forumId)
        {
            var forum = GetbyId(forumId);
            _context.Remove(forum);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ApplicationUser> GetActiveUsers(int id)
        {
            var posts = GetbyId(id).Posts;

            if (posts!=null || !posts.Any())
            {
                var postUsers = posts.Select(x => x.User);
                var replyUsers = posts.SelectMany(p => p.Replies).Select(r => r.User);

                return postUsers.Union(replyUsers).Distinct();
            }
            return new List<ApplicationUser>();
        }


        public IEnumerable<Forums> GetAll()
        {
            return _context.Forums.Include(forum => forum.Posts);
        }

        public Forums GetbyId(int Id)
        {
            var forum = _context.Forums.Where(x => x.Id == Id)
                .Include(f => f.Posts).ThenInclude(u => u.User)
                .Include(f => f.Posts).ThenInclude(u => u.Replies).ThenInclude(r => r.User)
                .FirstOrDefault();

            return forum;
        }

        public bool HasRecentPost(int id)
        {
            const int hoursAgo = 12;
            var window = DateTime.Now.AddHours(-hoursAgo);

            return GetbyId(id).Posts.Any(post => post.Created > window);
        }

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
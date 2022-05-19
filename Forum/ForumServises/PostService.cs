using Forum.Data;
using Forum.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.ForumServises
{
    public class PostService : IPosts
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context; 
        }


        public async Task AddPost(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public Task AddReplay(PostReply reply)
        {
            throw new NotImplementedException();
        }

        public Task DeletePost(int post)
        {
            throw new NotImplementedException();
        }

        public Task EditPostContent(int id, string newcContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {         
            return _context.Posts
                .Include(post => post.User)
                .Include(post => post.Replies).ThenInclude(reply => reply.User)
                .Include(post => post.Forums);
        }

        public Post GetById(int id)
        {

            return _context.Posts.Where(post => post.Id == id)
                .Include(post => post.User)
                .Include(post => post.Replies).ThenInclude(reply => reply.User)
                .Include(post => post.Forums).First();

        }

        public IEnumerable<Post> GetFiltredPosts(string serachQuery)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetLastestPost(int n)
        {
            return GetAll().OrderByDescending(post => post.Created).Take(n);
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            var posts = _context.Forums.Where(forum => forum.Id == id).First().Posts;

            return posts;
        }
    }
}

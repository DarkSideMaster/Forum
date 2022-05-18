using Forum.Data;
using Forum.Models;

namespace Forum.ForumServises
{
    public class PostService : IPosts
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context; 
        }


        public Task AddPost(Post post)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Post GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetFiltredPosts(string serachQuery)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            var posts = _context.Forums.Where(forum => forum.Id == id).First().Posts;

            return posts;
        }
    }
}

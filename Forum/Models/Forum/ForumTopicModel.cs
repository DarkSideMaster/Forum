using Forum.Models.Forum;
using Forum.Models.Posts;

namespace Forum.Models
{
    public class ForumTopicModel
    {
        public ForumsListinigModel Forum { set; get; }
        public IEnumerable<PostListingModel> Posts { set; get; }
    }
}

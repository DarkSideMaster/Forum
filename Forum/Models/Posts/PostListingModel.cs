using Forum.Models.Forum;

namespace Forum.Models.Posts
{
    public class PostListingModel
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Author { set; get; }
        public int AuthorRating { set; get; }
        public string AuthorId { set; get; }
        public string DatePosted { set; get; }

        public ForumsListinigModel Forum { set; get; }
        public int RepliesCount { set; get; }
    }
}

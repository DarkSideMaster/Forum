using Forum.Models.Reply;

namespace Forum.Models.Posts
{
    public class PostIndexModel
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string AuthorName { set; get; }
        public int AuthorRating { set; get; }
        public string AuthorId { set; get; }
        public DateTime Created { set; get; }
        public string AuthorImageUrl { set; get; }
        public string PostContent { set; get; }
        public int ForumId { set; get; }
        public string ForumName { set; get; }
        public bool IsAuthorAdmin { set; get; }

        public IEnumerable<PostReplyModel> Replies { set; get; }

    }
}

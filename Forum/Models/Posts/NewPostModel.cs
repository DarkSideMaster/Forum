namespace Forum.Models.Posts
{
    public class NewPostModel
    {
        public int ForumId { set; get; }
        public string ForumName { set; get; }
        public string AuthorName { set; get; }
        public string Title { set; get; }
        public string Content { set; get; }
        public string ForumImageUrl { set; get; }
    }
}

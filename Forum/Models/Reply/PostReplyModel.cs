namespace Forum.Models.Reply
{
    public class PostReplyModel
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string AuthorName { set; get; }
        public int AuthorRating { set; get; }
        public string AuthorId { set; get; }
        public DateTime Created { set; get; }
        public string AuthorImageUrl { set; get; }
        public string ReplyContent { set; get; }
        public bool IsAuthorAdmin { set; get; }
        public int PostId { set; get; }
        public string PostTitle { set; get; }
        public string PostContent { set; get; }
        public string ForumName { set; get; }
        public string ForumImageUrl { set; get; }
        public int ForumId { set; get; }
    }
}

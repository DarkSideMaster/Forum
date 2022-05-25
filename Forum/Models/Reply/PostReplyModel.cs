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
        public string ReplayContent { set; get; }

        public bool IsAuthorAdmin { set; get; }
        public int PostId { set; get; }
    }
}

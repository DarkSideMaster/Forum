namespace Forum.Models
{
    public class PostReply
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime Created { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual Forums? Forums { set; get; }
        public virtual List<PostReply>? Replies { set; get; }
    }
}

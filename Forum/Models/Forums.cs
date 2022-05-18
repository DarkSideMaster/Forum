namespace Forum.Models
{
    public class Forums
    {
        public int Id { get; set; }
        public string ?Title { get; set; }
        public string ?Description { get; set; }
        public DateTime Created { get; set; }
        public string ?ImageUrl { set; get; }
        public virtual List<Post>? Posts { set; get; }
    }
}

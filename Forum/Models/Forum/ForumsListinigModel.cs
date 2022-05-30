namespace Forum.Models.Forum
{
    public class ForumsListinigModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string ImageUrl { set; get; }
        public int NumberOfPosts { set; get; }
        public int NumberOfUsers { set; get; }
        public bool HasRecentPost { set; get; }
    }
}

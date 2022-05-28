namespace Forum.Models.Forum
{
    public class AddForumModel
    {
        public string Title { set; get; }
        public string Description { set; get; }
        public string ImageUrl { set; get; }
        public IFormFile ImageUpload { set; get; }

    }
}

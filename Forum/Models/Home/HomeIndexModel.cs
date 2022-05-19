using Forum.Models.Posts;

namespace Forum.Models.Home
{
    public class HomeIndexModel
    {
        public string SearchQuery { set; get; }

        public IEnumerable<PostListingModel> LatestPost { set; get; }
    }
}

using Forum.Models.Posts;

namespace Forum.Models.Search
{
    public class SearchResultModel
    {
        public string SearchQuery { set; get; }
        public bool EmptySearchResult { set; get; }
        public IEnumerable<PostListingModel> Posts{set;get;}

    }
}

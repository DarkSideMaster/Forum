using Forum.Data;
using Forum.Models;
using Forum.Models.Forum;
using Forum.Models.Posts;
using Forum.Models.Search;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class SearchController : Controller
    {

        private readonly IPosts _postService;

        public SearchController(IPosts postService)
        {
            _postService = postService;
        }


        public IActionResult Results(string searchQuery)
        {
            var posts = _postService.GetFiltredPosts(searchQuery);

            var isNoResults = (!string.IsNullOrEmpty(searchQuery) && !posts.Any());

            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                Author = post.User.UserName,
                AuthorRating = post.User.Rating,
                Title = post.Title,
                DatePosted = post.Created.ToShortDateString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post)
            });

            var model = new SearchResultModel 
            {
                Posts = postListings,
                SearchQuery = searchQuery,
                EmptySearchResult = isNoResults
            };

            return View(model); 
        }

        private ForumsListinigModel BuildForumListing(Post post)
        {
            var forum = post.Forums;

            return new ForumsListinigModel
            {
                Id=forum.Id,
                ImageUrl = forum.ImageUrl,
                Name = forum.Title,
                Description = forum.Description
            };
        }

        [HttpPost]
        public IActionResult Search(string searchQuery)
        {

            return RedirectToAction("Results", new { searchQuery });
        }
    }
}

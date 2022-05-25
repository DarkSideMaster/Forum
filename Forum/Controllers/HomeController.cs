using Forum.Data;
using Forum.Models;
using Forum.Models.Forum;
using Forum.Models.Home;
using Forum.Models.Posts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPosts _postService;


        public HomeController(ILogger<HomeController> logger, IPosts postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public IActionResult Index()
        {

            var model = BuildHomeIndexModel();

            return View(model);
        }

        private HomeIndexModel BuildHomeIndexModel()
        {
            var lastestPost = _postService.GetLastestPost(20);


            var posts = lastestPost.Select(post => new PostListingModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                Author = post.User.UserName,
                AuthorRating = post.User.Rating,
                DatePosted = post.Created.ToShortDateString(),
                RepliesCount = post.Replies.Count(),
                Forum = GetForumListingForPost(post)
            }) ;

            return new HomeIndexModel
            {
                LatestPost = posts,
                SearchQuery = ""
            };
        }

        private ForumsListinigModel GetForumListingForPost(Post posts)
        {
            var forum = posts.Forums;

            return new ForumsListinigModel
            {
                Name = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl,
                Id = forum.Id,
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
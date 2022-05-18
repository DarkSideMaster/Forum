using Forum.Data;
using Forum.Models;
using Forum.Models.Forum;
using Forum.Models.Posts;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class ForumsController : Controller
    {
        private readonly IForums _forumService;
        private readonly IPosts _postService;

        public ForumsController(IForums forumService) 
        {
            _forumService = forumService;
        }

        public IActionResult Index()
        {

            var forums = _forumService.GetAll().Select(forum=>new ForumsListinigModel 
            { 
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description
            });

            var model = new ForumsIndexModel
            {
                ForumsList = forums
            };

            return View(model);
        }


        public IActionResult Topic(int Id) 
        {
            var forums = _forumService.GetbyId(Id);
            var posts = forums.Posts;

            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                Title = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post)

            });

            var model = new ForumTopicModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forums)
            };

            return View(model);
        }

        private ForumsListinigModel BuildForumListing(Post post)
        {
            var forum = post.Forums;
           return BuildForumListing(forum);
        }

        private ForumsListinigModel BuildForumListing(Forums forum)
        {
            
            return new ForumsListinigModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
        }
    }
}

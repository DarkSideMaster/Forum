using Forum.Data;
using Forum.Data.Interfaces;
using Forum.Models;
using Forum.Models.Posts;
using Forum.Models.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPosts _postService;
        private readonly IForums _forumService;
        private static UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;

        public PostController(IPosts postService, IForums forumService, UserManager<ApplicationUser> userManager, IApplicationUser userService)
        {
            _postService = postService;
            _forumService = forumService;
            _userManager = userManager;
            _userService = userService;
        }

        public IActionResult Index( int id)
        {
            var  post = _postService.GetById(id);

            var replies = BuildPostReplies(post.Replies);

            var model = new PostIndexModel
            {
                Id=post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorImageUrl = string.IsNullOrEmpty(post.User.ProfileImageUrl) ? post.User.ProfileImageUrl = "/images/Users/defaultUser.png" : post.User.ProfileImageUrl,
                AuthorRating = post.User.Rating,
                Created = post.Created,
                PostContent = post.Content,
                Replies = replies,
                ForumId = post.Forums.Id,
                ForumName = post.Forums.Title,
                IsAuthorAdmin = IsAuthorAdmin(post.User)
            };

            return View(model);
        }

        private bool IsAuthorAdmin(ApplicationUser user)
        {         
            return _userManager.GetRolesAsync(user).Result.Contains("Admin");   
        }

        //id it is Forum.Id
        public IActionResult CreatePost(int id) 
        {       
            var forum = _forumService.GetbyId(id);

            var model = new NewPostModel
            {
                ForumId = forum.Id,
                ForumName =forum.Title,
                ForumImageUrl = forum.ImageUrl,
                AuthorName = User.Identity.Name //cool
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(NewPostModel model) 
        {
            var userId = _userManager.GetUserId(User);
            var user =  _userManager.FindByIdAsync(userId).Result;
            var post = BuildPost(model, user);

            _postService.AddPost(post).Wait();

            await _userService.UpdateUserRating(userId, typeof(Post));

            return RedirectToAction("Index", "Post", new { id = post.Id });
        }

        private Post BuildPost(NewPostModel model, ApplicationUser user)
        {
            var forum = _forumService.GetbyId(model.ForumId);

            return new Post
            {
                Title=model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                User = user,
                Forums = forum
            };
        }


        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply>? replies)
        {
            return replies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                AuthorName = reply.User.UserName,
                AuthorId = reply.User.Id,
                AuthorImageUrl = string.IsNullOrEmpty(reply.User.ProfileImageUrl) ? reply.User.ProfileImageUrl = "/images/Users/defaultUser.png" : reply.User.ProfileImageUrl,
                AuthorRating = reply.User.Rating,
                Created = reply.Created,
                ReplyContent = reply.Content,
                IsAuthorAdmin = IsAuthorAdmin(reply.User)
            }) ;
        }
    }
}

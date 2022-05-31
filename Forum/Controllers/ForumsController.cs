using Forum.Data;
using Forum.Data.Interfaces;
using Forum.Models;
using Forum.Models.Forum;
using Forum.Models.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Net.Http.Headers;

namespace Forum.Controllers
{
    public class ForumsController : Controller
    {
        private readonly IForums _forumService;
        private readonly IPosts _postService;
        private readonly IUpload _uploadService;
        private readonly IConfiguration _configuration;

        public ForumsController(IForums forumService, IPosts postService, IUpload uploadService, IConfiguration configuration)
        {
            _forumService = forumService;
            _postService = postService;
            _uploadService = uploadService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {

            var forums = _forumService.GetAll().Select(forum => new ForumsListinigModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description,
                NumberOfPosts = forum.Posts?.Count() ?? 0,
                NumberOfUsers = _forumService.GetActiveUsers(forum.Id).Count(),
                ImageUrl = forum.ImageUrl,
                HasRecentPost = _forumService.HasRecentPost(forum.Id)
            });

            var model = new ForumsIndexModel
            {
                ForumsList = forums.OrderBy(f=>f.Name)
            };

            return View(model);
        }


        public IActionResult Topic(int id, string searchQuery)
        {
            var forums = _forumService.GetbyId(id);

            var posts = new List<Post>();

            var isSucssesSearch = true; 

            if (!string.IsNullOrEmpty(searchQuery))
            {
                 posts = _postService.GetFiltredPosts(id, searchQuery).ToList();
                if (posts!=null && posts.Count==0)
                {
                    isSucssesSearch = false;
                }
            }
            else
            {
                posts = forums.Posts;
            }
            
            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                Title = post.Title,
                RepliesCount = post.Replies.Count(),
                DatePosted = post.Created.ToShortDateString(),
                Author = post.User.UserName,             
                Forum = BuildForumListing(post)

            });

            var model = new ForumTopicModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forums),
                isSearchSucsses = isSucssesSearch,
                SearchQuery = searchQuery
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

        [HttpPost]
        public IActionResult Search(int id, string searchQuery) 
        {
            return RedirectToAction("Topic", new { id, searchQuery });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() 
        {
            var model = new AddForumModel();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddForum(AddForumModel model) 
        {
            var imageUrl = "/images/Users/defaultUser.png"; ;

            if (string.IsNullOrEmpty(model.ImageUrl))
            {
                var blockBlob = UploadForumImage(model.ImageUpload);
                imageUrl = blockBlob.Uri.AbsoluteUri;
            }

            var forum = new Forums
            {
                Title = model.Title,
                Description = model.Description,
                Created = DateTime.Now,
                ImageUrl = imageUrl
            };

            await _forumService.Create(forum);

            return RedirectToAction("Index", "Forums");
        }


        //Upload to the Azure Clouds
        private CloudBlockBlob UploadForumImage(IFormFile file)
        {
            //Connect Azure Storage Account Container
            var connectionString = _configuration.GetConnectionString("AzureStorageAccount");

            //Get Blob Container
            var container = _uploadService.GetBlobContainer(connectionString, "forum-images");

            //Parse the Content Disposition response header
            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            //Grab the filename
            var filename = contentDisposition.FileName.Trim('"');

            //Get a reference to Block Blob
            var blockBlob = container.GetBlockBlobReference(filename);

            //On taht block blob, upload our file <-- file uploaded to the cloud
             blockBlob.UploadFromStreamAsync(file.OpenReadStream()).Wait();

            return blockBlob;
        }
    }
}

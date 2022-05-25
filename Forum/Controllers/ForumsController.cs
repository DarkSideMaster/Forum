﻿using Forum.Data;
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

        public ForumsController(IForums forumService, IPosts postService)
        {
            _forumService = forumService;
            _postService = postService;
        }

        public IActionResult Index()
        {

            var forums = _forumService.GetAll().Select(forum => new ForumsListinigModel
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
    }
}

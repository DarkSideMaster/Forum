using Forum.Data;
using Forum.Models;
using Forum.Models.Forum;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class ForumsController : Controller
    {
        private readonly IForums _forumService;

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

        }
    }
}

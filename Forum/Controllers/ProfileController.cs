using Forum.Data.Interfaces;
using Forum.Models;
using Forum.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class ProfileController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;

        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUser userServise, IUpload uploadService)
        {
            _userManager = userManager;
            _userService = userServise;
            _uploadService = uploadService;
        }

        public IActionResult Detail(string id)
        {
            var model = new ProfileModel()
            {

            };


            return View(model);
        }
    }
}

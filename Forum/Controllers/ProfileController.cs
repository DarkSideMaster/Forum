using Forum.Data.Interfaces;
using Forum.Models;
using Forum.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Forum.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;
        private readonly IConfiguration _configuration;

        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUser userServise, IUpload uploadService, IConfiguration configuration)
        {
            _userManager = userManager;
            _userService = userServise;
            _uploadService = uploadService;
            _configuration = configuration;
        }

        public IActionResult Detail(string id)
        {
            var user = _userService.GetById(id);
            var userRoles = _userManager.GetRolesAsync(user).Result;

            var model = new ProfileModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserRating = user.Rating.ToString(),
                Email = user.Email,
                ProfileImageUrl = string.IsNullOrEmpty(user.ProfileImageUrl)? user.ProfileImageUrl ="/images/Users/defaultUser.png" : user.ProfileImageUrl,
                MemberSince = user.MemderSince,
                IsAdmin = userRoles.Contains("Admin")
            };
            return View(model);
        }


        //Upload image to the Azure Cloud
        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            var userId = _userManager.GetUserId(User);

            var containerBlobName = "profile-images";
            //Connect Azure Storage Account Container
            var connectionString = _configuration.GetConnectionString("AzureStorageAccount");

            //Get Blob Container
            var container = _uploadService.GetBlobContainer(connectionString, containerBlobName);

            //Parse the Content Disposition response header
            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            //Grab the filename
            var filename = contentDisposition.FileName.Trim('"');

            //Get a reference to Block Blob
            var blockBlob = container.GetBlockBlobReference(filename);

            //On taht block blob, upload our file <-- file uploaded to the cloud
            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            await _userService.SetProfileImage(userId, blockBlob.Uri);

            //Redirect to the user's profile page
            return RedirectToAction("Detail", "Profile", new { id = userId });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index() 
        {
            var profiles = _userService.GetAll().OrderByDescending(user=>user.UserName)
                .Select(u=>new ProfileModel 
                {
                    Email=u.Email,
                    UserName=u.UserName,
                    ProfileImageUrl = u.ProfileImageUrl,
                    UserRating = u.Rating.ToString(),
                    MemberSince = u.MemderSince,
                    IsAdmin = _userManager.GetRolesAsync(u).Result.Contains("Admin")
        });

            var model = new ProfileListModel
            {
                Profiles = profiles,
            };

            return View(model);
        }
    }
}

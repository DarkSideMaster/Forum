﻿using Forum.Data.Interfaces;
using Forum.ForumHelpers;
using Forum.Models;
using Forum.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Forum.Controllers
{
    public class RoleUsersController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;
        private readonly IConfiguration _configuration;

        public RoleUsersController(UserManager<ApplicationUser> userManager, IApplicationUser userServise, IUpload uploadService, IConfiguration configuration)
        {
            _userManager = userManager;
            _userService = userServise;
            _uploadService = uploadService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var model = new ProfileListModel();
            var getRoles = new Roles();

            var profiles = _userService.GetAll().OrderByDescending(user => user.UserName)
                .Select(u => new ProfileModel
                {
                    Email = u.Email,
                    UserName = u.UserName,
                    ProfileImageUrl = u.ProfileImageUrl,
                    UserRating = u.Rating.ToString(),
                    MemberSince = u.MemderSince,
                    IsAdmin = _userManager.GetRolesAsync(u).Result.Contains("Admin"),
                    IsModerator = _userManager.GetRolesAsync(u).Result.Contains("Moderator"),
                    IsSuperModerator = _userManager.GetRolesAsync(u).Result.Contains("SuperModerator"),
                });


            var getRolesList = getRoles.GetAllRoles();

            model.RolesSelectList = new List<SelectListItem>();
            model.Profiles = profiles;

            foreach (var currentRole in getRolesList)
            {
                model.RolesSelectList.Add(new SelectListItem
                    { Text = currentRole.RoleName, Value = currentRole.Id.ToString()});
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AddRoleToUser(ProfileListModel model)
        {
            var selectedRole = model.RolesSelectList;
            
            return RedirectToAction("Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Forum.Models.User
{
    public class ProfileListModel
    {
        public IEnumerable<ProfileModel> Profiles { set; get; }
        public List<SelectListItem> RolesSelectList { set; get; }
    }
}

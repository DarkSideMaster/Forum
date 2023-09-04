using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models.User
{
    public class ProfileListModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public IEnumerable<ProfileModel> Profiles { set; get; }
        public List<SelectListItem> RolesSelectList { set; get; }

    }
}

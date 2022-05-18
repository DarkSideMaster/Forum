using Microsoft.AspNetCore.Identity;

namespace Forum.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Rating { set; get; }
        public string ProfileImageUrl { set; get; }
        public DateTime MemderSince { set; get; }
        public bool isActive { set; get; }
    }
}

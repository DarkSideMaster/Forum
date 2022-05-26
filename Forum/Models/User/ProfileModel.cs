namespace Forum.Models.User
{
    public class ProfileModel
    {
        public string UserId { set; get; }
        public string Email { set; get; }
        public string UserName { set; get; }
        public string UserNickName { set; get; }
        public string UserRating { set; get; }
        public string ProfileImageUrl { set; get; }
        public DateTime MemberSince { set; get; }
        public IFormFile ImageUpload { set; get; }

    }
}

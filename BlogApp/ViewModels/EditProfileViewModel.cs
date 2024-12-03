namespace BlogApp.ViewModels
{
    public class EditProfileViewModel
    {
        public string UserName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public IFormFile? ProfileImage { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels
{
    public class CreateUserViewModel
    {   
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]

        public string? ConfirmPassword { get; set; }

    }
}
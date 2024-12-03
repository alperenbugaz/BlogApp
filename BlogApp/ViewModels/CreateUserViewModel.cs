using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels
{
    public class CreateUserViewModel
    {   
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }

    }
}
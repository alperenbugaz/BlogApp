using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels
{
    public class RegisterViewModel
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Surname { get; set; }  = null!;

        [Compare("Password",ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }

    }
}
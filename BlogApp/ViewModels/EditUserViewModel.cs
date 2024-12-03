namespace BlogApp.ViewModels
{
    public class EditUserViewModel
    {   
        public string? Id { get; set; }
        public string? UserName { get; set; }

        public IList<string>? SelectedRoles { get; set; }
    }
}
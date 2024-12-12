using BlogApp.Data.Concrete;


namespace BlogApp.ViewModels
{
    public class SubscriptionViewModel
    {
        public List<BlogAppUser> Subscriptions { get; set; } = null!;
        public List<BlogAppUser> Subscribers { get; set; } = null!;
    }
}
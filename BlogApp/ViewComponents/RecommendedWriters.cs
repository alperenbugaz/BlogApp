using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.ViewComponents
{
    public class RecommendedWriters : ViewComponent
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly UserManager<BlogAppUser> _userManager;

        public RecommendedWriters(ISubscriptionRepository subscriptionRepository, UserManager<BlogAppUser> userManager)
        {
            _subscriptionRepository = subscriptionRepository;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var subscriptions = _subscriptionRepository.GetSubscriptionsByUserId(user.Id);
                var subscribedUserIds = subscriptions.Select(s => s.SubscribedToId).ToList();

                // Abone olunmayan kullanıcıları al
                var recommendedWriters = _userManager.Users
                    .Where(u => u.Id != user.Id && !subscribedUserIds.Contains(u.Id))
                    .ToList();

                // Rastgele 3 profil seç
                var random = new Random();
                var randomWriters = recommendedWriters.OrderBy(x => random.Next()).Take(3).ToList();

                return View(randomWriters);
            }
            else
            {
                // Kullanıcı giriş yapmadıysa rastgele 3 kullanıcı göster
                var allUsers = _userManager.Users.ToList();
                var random = new Random();
                var randomWriters = allUsers.OrderBy(x => random.Next()).Take(3).ToList();

                return View(randomWriters);
            }
        }
    }

}
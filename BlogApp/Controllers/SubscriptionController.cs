using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using BlogApp.Data.Entity;
using BlogApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private readonly UserManager<BlogAppUser> _userManager;
        private readonly ISubscriptionRepository _subscriptionRepository;

        private readonly INotificationRepository _notificationRepository;

        public SubscriptionController(UserManager<BlogAppUser> userManager, ISubscriptionRepository subscriptionRepository, INotificationRepository notificationRepository)
        {
            _userManager = userManager;
            _subscriptionRepository = subscriptionRepository;
            _notificationRepository = notificationRepository;

        }

        public async Task<IActionResult> Index(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            var subscriptions = _subscriptionRepository.GetSubscriptionsByUserId(user.Id);
            var subscribers = _subscriptionRepository.GetSubscribersByUserId(user.Id);

            var viewModel = new SubscriptionViewModel
            {
                Subscriptions = subscriptions.Select(s => s.SubscribedTo).ToList(),
                Subscribers = subscribers.Select(s => s.Subscriber).ToList()
            };

            return View(viewModel);

        }

        public async Task<IActionResult> Subscribe(string username)
        {
            var user = await _userManager.GetUserAsync(User);
            var subscribedTo = await _userManager.FindByNameAsync(username);

            if (user == null || subscribedTo == null)
            {
                return NotFound();
            }

            var subscription = new Subscription
            {
                SubscriberId = user.Id,
                SubscribedToId = subscribedTo.Id
            };


            _subscriptionRepository.AddSubscription(subscription);


            await _notificationRepository.CreateNotificationAsync(subscribedTo.Id, user.Id, "subscribed to you", null);


            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Unsubscribe(string username)
        {
            var user = await _userManager.GetUserAsync(User);
            var subscribedTo = await _userManager.FindByNameAsync(username);

            if (user == null || subscribedTo == null)
            {
                return NotFound();
            }

            var subscriptions = _subscriptionRepository.GetSubscriptionsByUserId(user.Id);

            var subscription = subscriptions.FirstOrDefault(s => s.SubscribedToId == subscribedTo.Id);

            if (subscription != null)
            {
                _subscriptionRepository.DeleteSubscription(subscription.Id);
            }

            return Redirect(Request.Headers["Referer"].ToString());

        }


    }
}
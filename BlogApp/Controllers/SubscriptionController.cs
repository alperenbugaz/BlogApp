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

        public SubscriptionController(UserManager<BlogAppUser> userManager , ISubscriptionRepository subscriptionRepository)
        {
            _userManager = userManager;
            _subscriptionRepository = subscriptionRepository;

        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new SubscriptionViewModel
            {
                Subscriptions = _subscriptionRepository.GetSubscriptionsByUserId(user.Id)
                    .Where(s => s.SubscriberId == user.Id)
                    .Select(s => s.SubscribedTo)
                    .ToList(),
                Subscribers = _subscriptionRepository.GetSubscriptionsByUserId(user.Id)
                    .Where(s => s.SubscribedToId == user.Id)
                    .Select(s => s.Subscriber)
                    .ToList()
            };

            return View(model);
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

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Unsubscribe(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            var subscribedTo = await _userManager.FindByIdAsync(id);

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

            return RedirectToAction("Index", "Home");
        }


    }
}
using BlogApp.Data.Abstract;
using BlogApp.Data.Entity;
using IdentityApp.Data.Concrete.EFCore;

namespace BlogApp.Data.Concrete.EFCore
{
    public class EfSubscriptionRepository : ISubscriptionRepository
    {   
        private readonly BlogAppContext _context;
        public EfSubscriptionRepository(BlogAppContext context) 
        {
            _context = context;
        }

        public void AddSubscription(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();
        }

        public void DeleteSubscription(int id)
        {
            var subscription = _context.Subscriptions.Find(id);
            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
                _context.SaveChanges();
            }
        }

        public List<Subscription> GetSubscriptionsByUserId(string id)
        {
            return _context.Subscriptions
                .Where(s => s.SubscriberId == id)
                .ToList();
        }

        public bool IsSubscribed(string subscriberId, string subscribedToId)
        {
            return _context.Subscriptions.Any(s => s.SubscriberId == subscriberId && s.SubscribedToId == subscribedToId);
        }
    }
}
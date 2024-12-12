using BlogApp.Data.Entity;

namespace BlogApp.Data.Abstract
{
    public interface ISubscriptionRepository
    {
        void AddSubscription(Subscription subscription);
        void DeleteSubscription(int id);
        List<Subscription> GetSubscriptionsByUserId(string id);

        public bool IsSubscribed(string subscriberId, string subscribedToId);
    }
}
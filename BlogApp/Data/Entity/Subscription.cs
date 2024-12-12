using System;
using BlogApp.Data.Concrete;

namespace BlogApp.Data.Entity
{
    public class Subscription
    {
        public int Id { get; set; }
        public string? SubscriberId { get; set; }
        public BlogAppUser? Subscriber { get; set; }
        public string? SubscribedToId { get; set; }
        public BlogAppUser? SubscribedTo { get; set; }
        public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
    }
}
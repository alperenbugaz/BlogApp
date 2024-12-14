using BlogApp.Data.Concrete;
using BlogApp.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Data.Concrete.EFCore
{
    public class BlogAppContext : IdentityDbContext<BlogAppUser,BlogAppRole,string>
    {
        public BlogAppContext(DbContextOptions<BlogAppContext> options) : base(options)
        {
            
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<Notification> Notifications { get; set; }
          protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Post - Tag
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Writer)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.WriterId)
                .OnDelete(DeleteBehavior.Restrict);

            // Post - Writer
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Writer)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.WriterId);
            // Post - Comment
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId);
            // User - Favorite
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId);
            // Post - Favorite
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Post)
                .WithMany(p => p.Favorites)
                .HasForeignKey(f => f.PostId);

            // Subscription - Subscriber
                        modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Subscriber)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(s => s.SubscriberId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.SubscribedTo)
                .WithMany(u => u.Subscribers)
                .HasForeignKey(s => s.SubscribedToId)
                .OnDelete(DeleteBehavior.Restrict);

                        // Notification - User
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Notification - Actor
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Actor)
            .WithMany()
            .HasForeignKey(n => n.ActorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Notification - Post
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Post)
            .WithMany()
            .HasForeignKey(n => n.PostId)
            .OnDelete(DeleteBehavior.SetNull);

        }
        
    }
}   
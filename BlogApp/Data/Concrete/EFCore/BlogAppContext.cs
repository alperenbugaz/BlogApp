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
          protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Writer)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.WriterId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Writer)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.WriterId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId);
        }
        
    }
}   
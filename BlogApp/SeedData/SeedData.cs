using BlogApp.Data.Concrete;
using BlogApp.Data.Entity;
using IdentityApp.Data.Concrete.EFCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.SeedData
{
    public static class BlogAppSeedData
    {
        private const string adminUserName = "admin";
        private const string adminPassword = "123456";
        private const string userUserName = "user";
        private const string userPassword = "123456";

        public static async void IdentityTestUser(IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<BlogAppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<BlogAppRole>>();
                var context = scope.ServiceProvider.GetRequiredService<BlogAppContext>();

                if(!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        new Category { Name = "C#" },
                        new Category { Name = "Java" },
                        new Category { Name = "Python" },
                        new Category { Name = "Javascript" },
                        new Category { Name = "C++" },
                        new Category { Name = "Ruby" },
                        new Category { Name = "Go" },
                        new Category { Name = "Swift" },
                        new Category { Name = "Kotlin" },
                        new Category { Name = "Rust" }
                    );
                    context.SaveChanges();
                }

                if (!context.Roles.Any())
                {
                    await roleManager.CreateAsync(new BlogAppRole { Name = "Admin" });
                    await roleManager.CreateAsync(new BlogAppRole { Name = "User" });
                }

                if (!context.Users.Any())
                {
                    var adminUser = new BlogAppUser
                    {
                        UserName = adminUserName,
                        Name = "Admin",
                        Surname = "Admin",
                        ProfileImageUrl = "/images/ProfilePhoto/pp.png",
                    };

                    var result = await userManager.CreateAsync(adminUser, adminPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }

                    var normalUser = new BlogAppUser
                    {
                        UserName = userUserName,
                        Name = "User",
                        Surname = "User",
                        ProfileImageUrl = "/images/ProfilePhoto/pp.png",
                    };

                    result = await userManager.CreateAsync(normalUser, userPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(normalUser, "User");
                    }
                    var normalUser2 = new BlogAppUser
                    {
                        UserName = "alperenbugaz",
                        Name = "Alperen",
                        Surname = "Buğaz",
                        ProfileImageUrl = "/images/alperen.jpeg",
                    };

                    result = await userManager.CreateAsync(normalUser2, "123456");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(normalUser2, "User");
                    }

                    var normalUser3 = new BlogAppUser
                    {
                        UserName = "ferhat",
                        Name = "Ferhat",
                        Surname = "Topcuoğlu",
                        ProfileImageUrl = "/images/ferhat.jpeg",
                    };

                    result = await userManager.CreateAsync(normalUser3, "123456");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(normalUser3, "User");
                    }


                }
            
                if(!context.Posts.Any())
                {
                    var normalUser2 = await userManager.FindByNameAsync("alperenbugaz");
                    var post = new Post
                    {
                        Title = "First Post",
                        Content = "Test Content",
                        Description = "Test Description",
                        Writer = normalUser2,
                        IsPublished = true,                        
                        CreatedAt = DateTime.Now,
                        ImageUrl = "/images/i3p4tnka.dqx.png",
                        CategoryId = 1
                    };
                    context.Posts.Add(post);
                    context.SaveChanges();
                }

                
                
            }
        }
    }
}
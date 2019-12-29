using Blog.Core.Domain.Posts;
using Blog.Core.Domain.Users;
using Blog.Data;
using Blog.Services.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Blog.API.Infrastructure.InitializeData
{
    /// <summary>
    /// Initialize db Data
    /// </summary>
    public static class InitializeDataExtensions
    {
        /// <summary>
        /// Databese deed data
        /// </summary>
        /// <param name="host">IWebHost</param>
        /// <returns>IWebHost</returns>
        public static IHost SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<IDbContext>();
                var signInManager = services.GetRequiredService<ISignInManager>();

                dbContext.AddInitializeUserDataAsync(signInManager).Wait();
                dbContext.AddInitializePostDataAsync().Wait();
            }
            return host;
        }

        private static async Task AddInitializeUserDataAsync(this IDbContext dbContext, ISignInManager signInManager)
        {
            var usersTable = dbContext.Set<User>();
            if (!(await usersTable.AnyAsync()))
            {
                var user1 = new User
                {
                    FirstName = "fn 1",
                    LastName = "ln 1",
                    UserName = "user1",
                    Email = "user1@user.com",
                    Active = true,
                    CreatedOnUtc = DateTime.UtcNow
                };
                signInManager.CreatePassword(user1, "123");

                var user2 = new User
                {
                    FirstName = "fn 2",
                    LastName = "ln 2",
                    UserName = "user2",
                    Email = "user2@user.com",
                    Active = true,
                    CreatedOnUtc = DateTime.UtcNow
                };
                signInManager.CreatePassword(user2, "123a");

                usersTable.AddRange(user1, user2);
                dbContext.SaveChanges();
            }
        }

        private static async Task AddInitializePostDataAsync(this IDbContext dbContext)
        {
            var postsTable = dbContext.Set<Post>();
            if (!(await postsTable.AnyAsync()))
            {
                var post1 = new Post
                {
                    Title = "t post1",
                    Body = "b post1",
                    BodyOverview = "bo post1",
                    Tags = "tg post1",
                    MetaTitle = "mt post1",
                    MetaKeywords = "mk post1",
                    MetaDescription = "md post1",
                    StartDateUtc = DateTime.UtcNow.AddHours(2),
                    CreatedOnUtc = DateTime.UtcNow,
                    EndDateUtc = DateTime.UtcNow.AddYears(5),
                    AllowComments = true,
                    Deleted = false
                };
                post1.PostComments.Add(new PostComment { CommentText = "c post1-1", CreatedOnUtc = DateTime.UtcNow, IsApproved = true });
                post1.PostComments.Add(new PostComment { CommentText = "c post1-2", CreatedOnUtc = DateTime.UtcNow, IsApproved = true });

                var post2 = new Post
                {
                    Title = "t post2",
                    Body = "b post2",
                    BodyOverview = "bo post2",
                    Tags = "tg post2",
                    MetaTitle = "mt post2",
                    MetaKeywords = "mk post2",
                    MetaDescription = "md post2",
                    StartDateUtc = DateTime.UtcNow.AddHours(2),
                    CreatedOnUtc = DateTime.UtcNow,
                    EndDateUtc = DateTime.UtcNow.AddYears(5),
                    AllowComments = true,
                    Deleted = false
                };
                post2.PostComments.Add(new PostComment { CommentText = "c post2-1", CreatedOnUtc = DateTime.UtcNow, IsApproved = true });
                post2.PostComments.Add(new PostComment { CommentText = "c post2-2", CreatedOnUtc = DateTime.UtcNow, IsApproved = true });

                var post3 = new Post
                {
                    Title = "t post3",
                    Body = "b post3",
                    BodyOverview = "bo post3",
                    Tags = "tg post3",
                    MetaTitle = "mt post3",
                    MetaKeywords = "mk post3",
                    MetaDescription = "md post3",
                    StartDateUtc = DateTime.UtcNow.AddHours(2),
                    CreatedOnUtc = DateTime.UtcNow,
                    EndDateUtc = DateTime.UtcNow.AddYears(5),
                    AllowComments = true,
                    Deleted = false
                };
                post3.PostComments.Add(new PostComment { CommentText = "c post3-1", CreatedOnUtc = DateTime.UtcNow, IsApproved = true });
                post3.PostComments.Add(new PostComment { CommentText = "c post3-2", CreatedOnUtc = DateTime.UtcNow, IsApproved = true });

                postsTable.AddRange(post1, post2, post3);
                dbContext.SaveChanges();
            }
        }
    }
}

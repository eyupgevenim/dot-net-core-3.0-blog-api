using Blog.Core.Domain.Posts;
using Blog.Data;
using Blog.Services.Posts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Blog.Services.Tests.Posts
{
    public class PostServiceTests
    {
        private IPostService _postService;
        private Mock<IRepository<Post>> _repositoryPost;

        private List<Post> GetPosts
        {
            get
            {
                var posts = new List<Post>();
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
                posts.Add(post1);

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
                posts.Add(post2);

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
                posts.Add(post3);

                return posts;
            }
        }

        public PostServiceTests()
        {
            _repositoryPost = new Mock<IRepository<Post>>();
            _repositoryPost.Setup(x => x.Table).Returns(GetPosts.AsQueryable());

            _postService = new PostService(_repositoryPost.Object);
        }

        [Fact]
        public void search_posts_by_title_and_paging()
        {
            var postPageResult = _postService.SearchPosts(title: "po", pageIndex: 0, pageSize: 2);
            Assert.NotNull(postPageResult);
            Assert.True(postPageResult.TotalPages == 2);
            Assert.True(postPageResult.TotalCount == 3);
            Assert.True(postPageResult.Items.Count == 2);
        }
    }
}

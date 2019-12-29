using System;
using System.Linq;
using Blog.Core;
using Blog.Core.Domain.Posts;
using Blog.Data;

namespace Blog.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _repositoryPost;
        public PostService(IRepository<Post> repositoryPost)
        {
            _repositoryPost = repositoryPost;
        }

        public Post GetPostById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("id");

            return _repositoryPost.GetById(id);
        }

        public void AddPost(Post post)
        {
            if (post == null)
                throw new ArgumentNullException("post");

            _repositoryPost.Insert(post);
        }

        public void DeletePost(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("id");

            var post = _repositoryPost.GetById(id);
            if (post == null)
                throw new Exception($"not found post id:{id}");

            post.Deleted = true;
            _repositoryPost.Update(post);
        }
        
        public PagingResult<Post> SearchPosts(string title = "", string body = "", string tags = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _repositoryPost.Table;

            if (!string.IsNullOrEmpty(title))
                query = query.Where(x => x.Title.Contains(title));

            if (!string.IsNullOrEmpty(body))
                query = query.Where(x => x.Body.Contains(body));

            if (!string.IsNullOrEmpty(tags))
                query = query.Where(x => x.Tags.Contains(tags));

            return new PagingResult<Post>(query, pageIndex, pageSize);
        }

        public void UpdatePost(Post post)
        {
            if (post == null)
                throw new ArgumentNullException("post");

            _repositoryPost.Update(post);
        }
    }
}

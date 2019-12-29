using Blog.Core;
using Blog.Core.Domain.Posts;

namespace Blog.Services.Posts
{
    public interface IPostService
    {
        Post GetPostById(int id);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int id);
        PagingResult<Post> SearchPosts(string title ="", string body ="",string tags = "", int pageIndex = 0, int pageSize = int.MaxValue);

    }
}

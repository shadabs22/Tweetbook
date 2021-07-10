using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetbook.Domain.v1;

namespace Tweetbook.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPosts();
        Task<Post> GetPostById(Guid postId);
        Task<bool> CreatePost(Post newPost);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(Guid postId);
    }
}

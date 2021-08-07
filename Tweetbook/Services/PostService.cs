using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Data;
using Tweetbook.Domain.v1;

namespace Tweetbook.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _dataContext;

        public PostService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Post>> GetPosts()
        {
            return _dataContext.Posts.Include(x => x.tags).ToList();
        }

        public async Task<Post> GetPostById(Guid postId)
        {
            return _dataContext.Posts.SingleOrDefault(x => x.id == postId);
        }

        public async Task<bool> CreatePost(Post newPost)
        {
            await _dataContext.Posts.AddAsync(newPost);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdatePost(Post newPost)
        {
            _dataContext.Posts.Update(newPost);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeletePost(Guid postId)
        {
            var post = await GetPostById(postId);
            if (post == null)
                return false;

            _dataContext.Posts.Remove(post);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UserOwnPostAsync(Guid postId, string userId)
        {
            var post = await _dataContext.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.id == postId);
            if (post == null)
            {
                return false;
            }
            if (post.UserId != userId)
            {
                return false;
            }
            return true;
        }
    }
}

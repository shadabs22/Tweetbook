using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tweetbook.Contracts.v1;
using Tweetbook.Contracts.v1.Requests;
using Tweetbook.Contracts.v1.Responses;

namespace Tweetbook.Sdk
{
    [Headers("Authorization: Bearer")]
    public interface ITweetbookApi
    {
        [Get(ApiRoutes.Posts.GetAll)]
        Task<ApiResponse<PostResponse>> GetAllAsync();

        [Get(ApiRoutes.Posts.Get)]
        Task<ApiResponse<PostResponse>> GetAsync(Guid postId);
        
        [Post(ApiRoutes.Posts.Create)]
        Task<ApiResponse<PostResponse>> CreateAsync([Body] CreatePostRequest createPostRequest);
        
        [Put(ApiRoutes.Posts.Update)]
        Task<ApiResponse<PostResponse>> UpdateAsync(Guid postId, [Body] UpdatePostRequest updatePostRequest);

        [Delete(ApiRoutes.Posts.Delete)]
        Task<ApiResponse<string>> DeleteAsync(Guid postId);

    }
}

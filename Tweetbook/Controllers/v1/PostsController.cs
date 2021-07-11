using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tweetbook.Contracts.v1;
using Tweetbook.Contracts.v1.Requests;
using Tweetbook.Contracts.v1.Responses;
using Tweetbook.Domain.v1;
using Tweetbook.Extensions;
using Tweetbook.Services;

namespace Tweetbook.Controllers.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_postService.GetPosts());
        }
        
        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = _postService.GetPostById(postId);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var userOwnPost = await _postService.UserOwnPostAsync(postId, HttpContext.GetUserId());
            if (!userOwnPost)
            {
                return BadRequest(new { error = "You don't own this post." });
            }
            var post = await _postService.GetPostById(postId);
            post.name = request.name;
            var updated = await _postService.UpdatePost(post);
            if (updated)
                return Ok(post);

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var userOwnPost = await _postService.UserOwnPostAsync(postId, HttpContext.GetUserId());
            if (!userOwnPost)
            {
                return BadRequest(new { error = "You don't own this post." });
            }
            var deleted = await _postService.DeletePost(postId);
            if (deleted)
                return NoContent();

            return NotFound();
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest createPost)
        {
            var post = new Post() { name = createPost.name, UserId = HttpContext.GetUserId() };
            if (post == null || post.id == Guid.Empty)
            {
                post.id = Guid.NewGuid();
            }
            await _postService.CreatePost(post);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.id.ToString());
            var postResponse = new PostResponse() { id = post.id };
            return Created(locationUri, postResponse);
        }
    }
}

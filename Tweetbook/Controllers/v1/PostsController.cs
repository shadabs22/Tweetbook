using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetbook.Contracts.v1;
using Tweetbook.Contracts.v1.Requests;
using Tweetbook.Contracts.v1.Responses;
using Tweetbook.Domain.v1;

namespace Tweetbook.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private List<Post> posts;
        public PostsController()
        {
            posts = new List<Post>();
            for(int i = 0; i < 5; i++)
            {
                posts.Add(new Post() { id = Guid.NewGuid().ToString() }) ;
            }
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(posts);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest createPost)
        {
            var post = new Post() { id = createPost.id };
            if (post == null || string.IsNullOrEmpty(post.id))
            {
                post = new Post() { id = "" + Guid.NewGuid() };
            }
            posts.Add(post);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.id);
            var postResponse = new PostResponse() { id = post.id };
            return Created(locationUri, postResponse);
        }
    }
}

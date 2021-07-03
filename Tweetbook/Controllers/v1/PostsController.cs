﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetbook.Contracts.v1;
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
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Contracts.v1;
using Tweetbook.Services;

namespace Tweetbook.Controllers.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [Authorize(Policy = "TagViewer", Roles ="Poster")]
        [HttpGet(ApiRoutes.Tags.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_tagService.GetTags());
        }

        //[Authorize(Policy = "TagViewer")]
        [Authorize(Roles = "Admin")]
        [HttpGet(ApiRoutes.Tags.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid tagId)
        {
            var tag = _tagService.GetTagById(tagId);
            if (tag == null)
                return NotFound();

            return Ok(tag);
        }

    }
}

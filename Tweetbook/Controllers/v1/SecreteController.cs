using Microsoft.AspNetCore.Mvc;
using Tweetbook.Filters;

namespace Tweetbook.Controllers.v1
{
    [ApiKeyAuth]
    public class SecreteController : ControllerBase
    {
        [HttpGet("secret")]
        public IActionResult GetSecret()
        {
            return Ok("I have no secrets");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextQuest.DTOs;
using NextQuest.Interfaces;

namespace NextQuest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostInterface _postInterface;

        public PostController(
            IPostInterface postInterface)
        {
            _postInterface = postInterface;
        }
        
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] PostDto request)
        {
            var post = _postInterface.MapDtoToModel(request);
            var response = await _postInterface.CreatePostAsync(post);
            
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            
            return Ok(response.Message);
        }
    }
}


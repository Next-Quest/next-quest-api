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
        public async Task<IActionResult> Post([FromBody] PostRequestDto request)
        {
            var userId = User.FindFirst("id")?.Value;
            if (!int.TryParse(userId, out var authorId))
                return Unauthorized();
            
            var post = _postInterface.MapPostRequestDtoToPostModel(request, authorId);
            
            var response = await _postInterface.CreatePostAsync(post);
            
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            
            return Ok(response.Message);
        }
        
        [HttpGet("get")]
        public async Task<IActionResult> GetPosts()
        {
            var response = await _postInterface.GetPostsAsync();
            
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            
            return Ok(response.Message);
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _postInterface.DeletePostAsync(id);
            
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            
            return Ok(response.Message);
        }
    }
}


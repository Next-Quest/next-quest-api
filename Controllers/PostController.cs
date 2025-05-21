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
        public async Task<IActionResult> GetPosts([FromQuery] int page, [FromQuery] int pageSize)
        {
            var response = await _postInterface.GetPostsAsync(page, pageSize);
            
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            
            return Ok(response.posts);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetPostByID(string id)
        {
            var response = await _postInterface.GetPostByIdAsync(id);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            
            return Ok(response.post);
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = User.FindFirst("id")?.Value;
            if (!int.TryParse(userId, out var authorId))
                return Unauthorized();
            
            var response = await _postInterface.DeletePostAsync(id, authorId);
            
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            
            return Ok(response.Message);
        }
    }
}


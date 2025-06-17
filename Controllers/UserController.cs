using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextQuest.Interfaces;
using NextQuest.Models;

namespace NextQuest.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserInterface _userInterface;
        
    public UserController(
        IUserInterface userInterface)
    {
        _userInterface = userInterface;
    }
    
    [Authorize]
    [HttpPost("setPreferred")]
    public async Task<IActionResult> TogglePreferredGenre([FromQuery] int genreId)
    {
        var tokenId = User.FindFirst("id")?.Value;
        if (!int.TryParse(tokenId, out var userId))
            return Unauthorized();
        
        var response = await _userInterface.TogglePreferredGenre(userId, genreId);
        
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
            
        return Ok(response.Message);
    }
}
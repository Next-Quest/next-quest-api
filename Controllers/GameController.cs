using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextQuest.DTOs.GameDtos;
using NextQuest.Interfaces;

namespace NextQuest.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameInterface _gameInterface;
    private readonly IUserInterface _userInterface;

    public GameController(
        IGameInterface gameInterface,
        IUserInterface userInterface)
    {
        _gameInterface = gameInterface;
        _userInterface = userInterface;
    }

    [Authorize]
    [HttpGet("create")]
    public async Task<IActionResult> Create([FromBody] CreateGameDto request)
    {
        var tokenId = User.FindFirst("id")?.Value;
        if (!int.TryParse(tokenId, out var userId))
            return Unauthorized();

        var isAdmin = await _userInterface.IsAdminAsync(userId); 
        
        if (!isAdmin.Success)
        {
            return Unauthorized();
        }

        var game = _gameInterface.MapCreateGameDtoToGameModel(request);

        var response  = await _gameInterface.CreateGameAsync(game);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
            
        return Ok(response.Message);
    }

    [HttpGet("listAll")]
    public async Task<IActionResult> List()
    {
        var response = await _gameInterface.GetAllGamesAsync();
        
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
            
        return Ok(new
        {
            Message = response.Message,
            Companies = response.Games
        });
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _gameInterface.GetGameByIdAsync(id);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        if (response.Game is null)
        {
            return Ok(response.Message);
        }
            
        return Ok(new
        {
            Message = response.Message,
            Game = response.Game
        });
    }

    [Authorize]
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateGameDto request)
    {
        var tokenId = User.FindFirst("id")?.Value;
        if (!int.TryParse(tokenId, out var userId))
            return Unauthorized();

        var isAdmin = await _userInterface.IsAdminAsync(userId); 
        
        if (!isAdmin.Success)
        {
            return Unauthorized();
        }

        request.Id = id;
        
        var response = await _gameInterface.UpdateGameAsync(request);
            
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
            
        return Ok(response.Message);
    }

    [Authorize]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var tokenId = User.FindFirst("id")?.Value;
        if (!int.TryParse(tokenId, out var userId))
            return Unauthorized();

        var isAdmin = await _userInterface.IsAdminAsync(userId); 
        
        if (!isAdmin.Success)
        {
            return Unauthorized();
        }
        
        var response = await _gameInterface.DeleteGameAsync(id);
        
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
            
        return Ok(response.Message);
    }
}
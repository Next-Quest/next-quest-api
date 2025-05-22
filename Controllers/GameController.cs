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

    public GameController(
        IGameInterface gameInterface)
    {
        _gameInterface = gameInterface;
    }

    [Authorize]
    [HttpGet("create")]
    public async Task<IActionResult> Create([FromBody] CreateGameDto request)
    {
        
    }

    [HttpGet("listAll")]
    public async Task<IActionResult> List()
    {
        
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        
    }

    [Authorize]
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateGameDto request)
    {
        
    }

    [Authorize]
    [HttpPut("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextQuest.DTOs.GameDtos;
using NextQuest.DTOs.GenreDtos;
using NextQuest.Interfaces;
using NextQuest.Migrations;

namespace NextQuest.Controllers;

[ApiController]
[Route("[controller]")]
public class GenreController : ControllerBase
{
    private readonly IGenreInterface _genreInterface;
    private readonly IUserInterface _userInterface;
        
    public GenreController(
        IGenreInterface genreInterface,
        IUserInterface userInterface)
    {
        _genreInterface = genreInterface;
        _userInterface = userInterface;
    }
    
    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateGenreDto request)
    {
        var tokenId = User.FindFirst("id")?.Value;
        if (!int.TryParse(tokenId, out var userId))
            return Unauthorized();

        var isAdmin = await _userInterface.IsAdminAsync(userId); 
        
        if (!isAdmin.Success)
        {
            return Unauthorized();
        }

        var genre = _genreInterface.MapCreateGenreDtoToGenreModel(request);

        var response  = await _genreInterface.CreateGenreAsync(genre);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
            
        return Ok(response.Message);
    }
    
    [HttpGet("listAll")]
    public IActionResult List()
    {
        var response = _genreInterface.GetAllGenres();
        
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
            
        return Ok(new
        {
            Message = response.Message,
            Genres = response.Genres
        });
    }
    
    [Authorize]
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateGenreDto request)
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
        
        var response = await _genreInterface.UpdateGenreAsync(request);
            
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
        
        var response = await _genreInterface.DeleteGenreAsync(id);
        
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
            
        return Ok(response.Message);
    }
}
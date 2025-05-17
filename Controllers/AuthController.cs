using Microsoft.AspNetCore.Mvc;
using NextQuest.Data;
using NextQuest.DTOs;
using NextQuest.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.Identity.Client;
using NextQuest.Interfaces;

namespace NextQuest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserInterface _userInterface;

        public AuthController(
            AppDbContext context,
            IUserInterface userInterface)
        {
            _context = context;
            _userInterface = userInterface;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            var response = await _userInterface.CreateUserAsync(request);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            
            return Ok(response.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto request)
        {
            var response = await _userInterface.LoginAsync(request);
            
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            
            return Ok(new
            {
                message = response.Message,
                token = response.Token
            });
        }

    }
}

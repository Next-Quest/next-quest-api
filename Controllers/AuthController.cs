using Microsoft.AspNetCore.Mvc;
using NextQuest.Data;
using NextQuest.DTOs;
using NextQuest.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace NextQuest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            // Validar e-mail
            if (!Regex.IsMatch(request.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return BadRequest("E-mail inválido.");

            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest("E-mail já cadastrado.");

            // Validar nome de usuário
            if (request.Username.Length < 4)
                return BadRequest("Nome de usuário deve ter pelo menos 4 caracteres.");

            if (!Regex.IsMatch(request.Username, @"^[a-zA-Z0-9_]+$"))
                return BadRequest("Nome de usuário só pode conter letras, números e _.");

            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                return BadRequest("Nome de usuário já está em uso.");

            // Validar senha
            if (request.Password.Length < 8 ||
                !Regex.IsMatch(request.Password, @"[a-zA-Z]") ||
                !Regex.IsMatch(request.Password, @"[0-9]") ||
                !Regex.IsMatch(request.Password, @"[\W_]"))
            {
                return BadRequest("Senha deve ter pelo menos 8 caracteres, incluindo letra, número e caractere especial.");
            }

            // Criptografar senha
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Criar novo usuário
            var user = new User
            {
                Email = request.Email,
                Username = request.Username,
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Usuário criado com sucesso.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto request)
        {
            // Verificar se o usuário existe
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return BadRequest("Usuário ou senha inválidos.");

            // Validar a senha
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return BadRequest("Usuário ou senha inválidos.");

            return Ok("Login bem-sucedido.");
        }

    }
}

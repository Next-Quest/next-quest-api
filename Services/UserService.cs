using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NextQuest.Data;
using NextQuest.DTOs;
using NextQuest.DTOs.UserDtos;
using NextQuest.Interfaces;
using NextQuest.Models;

namespace NextQuest.Services;

public class UserService : IUserInterface
{
    private readonly AppDbContext _context;
    private readonly IAuthInterface _auth;

    public UserService(
        AppDbContext context,
        IAuthInterface auth)
    {
        _context = context;
        _auth = auth;
    }

    public async Task<(bool Success, string Message)> CreateUserAsync(CreateUserDto request)
    {
        if (!Regex.IsMatch(request.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return (false, "E-mail inv�lido.");

        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            return (false, "E-mail j� cadastrado.");
        
        if (request.Username.Length < 4)
            return (false, "Nome de usu�rio deve ter pelo menos 4 caracteres.");

        if (!Regex.IsMatch(request.Username, @"^[a-zA-Z0-9_]+$"))
            return (false, "Nome de usu�rio s� pode conter letras, n�meros e _.");

        if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            return (false, "Nome de usu�rio j� est� em uso.");
        
        if (request.Password.Length < 8 ||
            !Regex.IsMatch(request.Password, @"[a-zA-Z]") ||
            !Regex.IsMatch(request.Password, @"[0-9]") ||
            !Regex.IsMatch(request.Password, @"[\W_]"))
        {
            return (false, "Senha deve ter pelo menos 8 caracteres, incluindo letra, n�mero e caractere especial.");
        }
        
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        var user = new User
        {
            Email = request.Email,
            Username = request.Username,
            PasswordHash = passwordHash
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return (true, "Usu�rio criado com sucesso.");
    }

    public async Task<(bool Success, string Message, string? Token)> LoginAsync(CreateUserDto request)
    {
        if (!request.Email.IsNullOrEmpty() && !request.Username.IsNullOrEmpty())
        {
            return (false, "Apenas e-mail ou nome de usu�rio deve ser preenchido com a senha.", null);
        }
        
        if (request.Email.IsNullOrEmpty() && request.Username.IsNullOrEmpty())
        {
            return (false, "E-mail ou nome de usu�rio � obrigat�rio.", null);
        }

        if (request.Password.IsNullOrEmpty())
        {
            return (false, "Senha � obrigat�ria.", null);
        }
        
        var user = !request.Email.IsNullOrEmpty() ?
            await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email) :
            await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user == null)
            return (false, "Usu�rio ou senha inv�lidos.", null);
        
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return (false, "Usu�rio ou senha inv�lidos.", null);
        
        var token = _auth.GenerateToken(user);
        
        return (true, "Login bem-sucedido.", token);
    }

    public async Task<(bool Success, string Message)> IsAdminAsync(int userId)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return (false, "N�o foi poss�vel identificar esse usu�rio.");
            }
            
            return user.IsAdmin ? (true, "Usu�rio � administrador.") : (false, "Usu�rio n�o possui permiss�o para realizar essa a��o.");
        }
        catch (Exception e)
        {
            return (false, e.Message);  
        }
    }
}
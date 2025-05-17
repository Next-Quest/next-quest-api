using NextQuest.DTOs;
using NextQuest.Models;

namespace NextQuest.Interfaces;

public interface IAuthInterface
{
    public string GenerateToken(User user);
}
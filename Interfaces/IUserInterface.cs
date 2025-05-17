using NextQuest.DTOs;

namespace NextQuest.Interfaces;

public interface IUserInterface
{
    public Task<(bool Success, string Message)> CreateUserAsync(UserDto request);
    public Task<(bool Success, string Message, string? Token)> LoginAsync(UserDto request);
}
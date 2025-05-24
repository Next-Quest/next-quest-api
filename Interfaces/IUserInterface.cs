using NextQuest.DTOs;
using NextQuest.DTOs.UserDtos;

namespace NextQuest.Interfaces;

public interface IUserInterface
{
    public Task<(bool Success, string Message)> CreateUserAsync(CreateUserDto request);
    public Task<(bool Success, string Message, string? Token)> LoginAsync(CreateUserDto request);
    public Task<(bool Success, string Message)> IsAdminAsync(int userId);

}
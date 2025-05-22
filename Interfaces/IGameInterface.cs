using NextQuest.DTOs.CompanyDtos;
using NextQuest.DTOs.GameDtos;
using NextQuest.Models;

namespace NextQuest.Interfaces;

public interface IGameInterface
{
    public Task<(bool Success, string Message)> CreateGameAsync(Game game);
    public Task<(bool Success, string Message, List<GameDto>? Games)> GetAllGamesAsync();
    public Task<(bool Success, string Message, GameDto? Company)> GetGameByIdAsync(int gameId);
    public Task<(bool Success, string Message)> UpdateGameAsync(UpdateGameDto gameDto);
    public Task<(bool Success, string Message)> DeleteGameAsync(int gameId);

    #region DtoMapping
    public Game MapCreateGameDtoToGameModel(CreateGameDto gameDto);
    #endregion
}
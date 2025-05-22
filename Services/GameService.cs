using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NextQuest.Data;
using NextQuest.DTOs.GameDtos;
using NextQuest.Interfaces;
using NextQuest.Models;

namespace NextQuest.Services;

public class GameService : IGameInterface
{
    private readonly AppDbContext _context;

    public GameService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message)> CreateGameAsync(Game game)
    {
        try
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
        
            return (true, "Jogo criado com sucesso.");
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool Success, string Message, List<GameDto>? Games)> GetAllGamesAsync()
    {
        try
        {
            var response = await _context.Games.ToListAsync();

            if (response.Count == 0)
            {
                return (false, "Nenhum jogo encontrado.", null);
            }

            var games = MapGameModelToGameDtoList(response);
            
            return (true, "Jogos encontrados com sucesso.", games);
        }
        catch (Exception e)
        {
            return (false, e.Message, null);
        }
    }

    public async Task<(bool Success, string Message, GameDto? Game)> GetGameByIdAsync(int gameId)
    {
        try
        {
            var response = await _context.Games.FindAsync(gameId);

            if (response == null)
            {
                return (true, "Nenhum jogo encontrado.", null);
            }
            
            var game = MapGameModelToGameDto(response);
            
            return (true, "Jogo encontrado com sucesso.", game);
        }
        catch (Exception e)
        {
            return (false, e.Message, null);
        }
    }

    public async Task<(bool Success, string Message)> UpdateGameAsync(UpdateGameDto gameDto)
    {
        try
        {
            var existingGame = await _context.Games.FindAsync(gameDto.Id);
            if (existingGame == null)
            {
                return (false, "Jogo não encontrado.");
            }

            if (gameDto.Title.IsNullOrEmpty() &&
                !gameDto.PublisherId.HasValue &&
                !gameDto.DeveloperId.HasValue  &&
                !gameDto.ReleaseDate.HasValue)
            {
                return (false, "Nenhuma informação a ser atualizada.");
            }

            if (!gameDto.Title.IsNullOrEmpty())
            {
                existingGame.Title = gameDto.Title;
            }

            if (gameDto.PublisherId.HasValue)
            {
                existingGame.PublisherId = gameDto.PublisherId.Value;
            }

            if (gameDto.DeveloperId.HasValue)
            {
                existingGame.DeveloperId = gameDto.DeveloperId.Value;
            }

            if (gameDto.ReleaseDate.HasValue)
            {
                existingGame.ReleaseDate = gameDto.ReleaseDate.Value;
            }
            
            await _context.SaveChangesAsync();
            return (true, "Jogo atualizado com sucesso.");
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool Success, string Message)> DeleteGameAsync(int gameId)
    {
        try
        {
            var existingGame = await _context.Games.FindAsync(gameId);
            if (existingGame == null)
            {
                return (false, "Jogo não encontrado.");
            }
            
            _context.Games.Remove(existingGame);
            await _context.SaveChangesAsync();
            
            return (true, "Jogo excluído com sucesso.");
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    #region DtoMapping

    private GameDto MapGameModelToGameDto(Game game)
    {
        return new GameDto()
        {
            Id = game.Id,
            Title = game.Title,
            PublisherId = game.PublisherId,
            DeveloperId = game.DeveloperId,
            ReleaseDate = game.ReleaseDate
        };
    }

    private List<GameDto> MapGameModelToGameDtoList(List<Game> games)
    {
        var gameDtoList = new List<GameDto>();

        foreach (var game in games)
        {
            gameDtoList.Add(MapGameModelToGameDto(game));
        }
        
        return gameDtoList;
    }
    
    public Game MapCreateGameDtoToGameModel(CreateGameDto gameDto)
    {
        return new Game()
        {
            Title = gameDto.Title,
            PublisherId = gameDto.PublisherId,
            DeveloperId = gameDto.DeveloperId,
            ReleaseDate = gameDto.ReleaseDate
        };
    }
    
    #endregion
}
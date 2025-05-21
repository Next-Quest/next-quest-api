using NextQuest.Data;
using NextQuest.Models;

namespace NextQuest.Services;

public class GameService
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
}
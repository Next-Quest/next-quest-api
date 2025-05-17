using NextQuest.Data;
using NextQuest.Interfaces;

namespace NextQuest.Services;

public class UserService : IUserInterface
{
    private readonly AppDbContext _context;
    private readonly IUserInterface _userInterface;

    public UserService(
        AppDbContext context,
        IUserInterface userInterface)
    {
        _context = context;
        _userInterface = userInterface;
    }
    
    
}
using NextQuest.Models;

namespace NextQuest.DTOs;

public class PostDto
{
    public string? Id { get; set; }
    public int AuthorId { get; set; }
    public int GameId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<int>? Likes { get; set; }
    public List<Comment>? Comments { get; set; }
    public DateTime CreatedAt { get; set; }
}
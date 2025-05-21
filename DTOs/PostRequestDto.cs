namespace NextQuest.DTOs;

public class PostRequestDto
{
    public string? Id { get; set; }
    public int AuthorId { get; set; }
    public int GameId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}
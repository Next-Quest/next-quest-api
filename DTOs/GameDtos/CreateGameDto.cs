namespace NextQuest.DTOs.GameDtos;

public class CreateGameDto
{
    public string Title { get; set; } = "";
    public int PublisherId { get; set; }
    public int DeveloperId { get; set; }
    public DateTime ReleaseDate { get; set; }
}
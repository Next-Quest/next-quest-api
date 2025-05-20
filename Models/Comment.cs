namespace NextQuest.Models;

public class Comment
{
    public int AuthorId { get; set; }
    public string Content { get; set; } = string.Empty;
    public List<int> LikedByIds { get; set; } = new List<int>();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
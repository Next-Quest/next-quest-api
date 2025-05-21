using System.ComponentModel.DataAnnotations;

namespace NextQuest.Models;

public class Game
{
    public int Id { get; set; }
        
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = "";
        
    [Required]
    public int PublisherId { get; set; }
        
    [Required]
    public int DeveloperId { get; set; }
        
    [Required]
    public DateTime ReleaseDate { get; set; }
}
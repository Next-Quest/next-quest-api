using System.ComponentModel.DataAnnotations;

namespace NextQuest.Models;

public class Genre
{
    public int Id { get; set; }
        
    [Required]
    [MaxLength(50)]
    public string GenreName { get; set; } = "";
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
using System.ComponentModel.DataAnnotations;

namespace NextQuest.Models;

public class Company
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
}
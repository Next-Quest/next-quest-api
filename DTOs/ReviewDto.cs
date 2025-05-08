using System.ComponentModel.DataAnnotations;

namespace NextQuest.DTOs
{
    public class ReviewDto
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Título deve ter até 30 caracteres")]
        public string Title { get; set;} = String.Empty;

        [Required]
        [MaxLength(500, ErrorMessage = "Comentário deve ter até 500 caracteres")]
        public string Comment { get; set;} = String.Empty;
    }
}
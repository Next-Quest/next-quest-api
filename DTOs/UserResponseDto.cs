namespace NextQuest.DTOs
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }
}
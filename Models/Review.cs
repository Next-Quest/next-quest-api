namespace NextQuest.Models
{
    public class Review
    {
        public int Id{ get; set;}
        public string Title { get; set;} = string.Empty;
        public string Comment { get; set;} = string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
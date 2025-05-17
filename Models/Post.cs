using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace NextQuest.Models;

public class Post
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("AuthorId")]
    [JsonPropertyName("AuthorId")]
    [BsonRequired]
    public int AuthorId { get; set; }
    
    [BsonElement("GameId")]
    [JsonPropertyName("GameId")]
    public int GameId { get; set; }
    
    [BsonElement("Title")]
    [JsonPropertyName("Title")]
    public string? Title { get; set; }
    
    [BsonElement("Description")]
    [JsonPropertyName("Description")]
    public string? Description { get; set; }
    
    [BsonElement("Likes")]
    [JsonPropertyName("Likes")]
    public List<int>? Likes { get; set; }
    
    [BsonElement("Comments")]
    [JsonPropertyName("Comments")]
    public List<Comment>? Comments { get; set; }
    
    [BsonElement("CreatedAt")]
    [JsonPropertyName("CreatedAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
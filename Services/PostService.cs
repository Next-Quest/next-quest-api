using NextQuest.DTOs;
using NextQuest.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using NextQuest.Models;

namespace NextQuest.Services;

public class PostService : IPostInterface
{
    private readonly IMongoCollection<Post> _postCollection;
    
    public PostService(
        IOptions<MongoDBSettings> mongoDBSettings,
        IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _postCollection = database.GetCollection<Post>("post");
    }
    
    public async Task<(bool Success, string Message)> CreatePostAsync(Post post)
    {
        try
        {
            post.CreatedAt = DateTime.UtcNow;
            await _postCollection.InsertOneAsync(post);
            
            return (true, "Publicação criada com sucesso.");
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public Post MapPostRequestDtoToPostModel(PostRequestDto dto, int authorId)
    {
        return new Post()
        {
            AuthorId = authorId,
            GameId = dto.GameId,
            Title = dto.Title,
            Description = dto.Description,
        };
    }
}
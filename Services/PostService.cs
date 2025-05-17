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
        IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _postCollection = database.GetCollection<Post>("post");
    }
    
    public async Task<(bool Success, string Message)> CreatePostAsync(Post post)
    {
        try
        {
            post.AuthorId = 1;
            post.Title = "Título";
            post.Description = "Descrição";
            
            await _postCollection.InsertOneAsync(post);
            
            return (true, "Publicação criada com sucesso.");
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public Post MapDtoToModel(PostDto dto)
    {
        return new Post()
        {
            Id = dto.Id,
            AuthorId = dto.AuthorId,
            GameId = dto.GameId,
            Title = dto.Title,
            Description = dto.Description,
            Likes = dto.Likes,
            Comments = dto.Comments,
            CreatedAt = dto.CreatedAt
        };
    }
}
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
            
            return (true, "Post criado com sucesso.");
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool Success, string Message, List<PostDto>? posts)> GetPostsAsync(int page, int pageSize)
    {
        try
        {
            var skip = (page - 1) * pageSize;
            
            var response = await _postCollection
                .Find(post => true)
                .SortByDescending(post => post.CreatedAt)
                .Skip(skip)
                .Limit(pageSize)
                .ToListAsync();
            
            var posts = MapPostModelToPostDtoList(response);
            return (true, "Posts recuperados com sucesso.", posts);
        }
        catch (Exception e)
        {
            return (false, e.Message, null);
        }
    }

    public async Task<(bool Success, string Message, PostDto? post)> GetPostByIdAsync(string id)
    {
        try
        {
            var response = await _postCollection.Find(post => post.Id == id).FirstOrDefaultAsync();
            
            var post = MapPostModelToPostDto(response);
            return (true, "Post recuperado com sucesso.", post);
        }
        catch (Exception e)
        {
            return (false, e.Message, null);
        }
    }
    
    public async Task<(bool Success, string Message)> DeletePostAsync(string postId)
    {
        try
        {
            await _postCollection.DeleteOneAsync(p => p.Id == postId);
            return (true, "Post excluído com sucesso.");
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public PostDto MapPostModelToPostDto(Post post)
    {
        return new PostDto
        {
            Id = post.Id,
            AuthorId = post.AuthorId,
            GameId = post.GameId,
            Title = post.Title,
            Description = post.Description,
            Likes = post.Likes,
            Comments = post.Comments,
            CreatedAt = post.CreatedAt
        };
    }

    public List<PostDto> MapPostModelToPostDtoList(List<Post> posts)
    {
        var postDtoList = new List<PostDto>();

        foreach (var post in posts)
        {
            postDtoList.Add(MapPostModelToPostDto(post));
        }
        
        return postDtoList;
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
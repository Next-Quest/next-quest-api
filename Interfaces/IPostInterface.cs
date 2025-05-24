using NextQuest.DTOs;
using NextQuest.DTOs.PostDtos;
using NextQuest.Models;

namespace NextQuest.Interfaces;

public interface IPostInterface
{
    public Task<(bool Success, string Message)> CreatePostAsync(Post post);
    public Task<(bool Success, string Message, List<PostDto>? posts)> GetPostsAsync(int page, int pageSize);
    public Task<(bool Success, string Message, PostDto? post)> GetPostByIdAsync(string id);
    public Task<(bool Success, string Message)> UpdatePostAsync(Post updatedPost);
    public Task<(bool Success, string Message)> DeletePostAsync(string postId, int authorId);
    public PostDto MapPostModelToPostDto(Post post);
    public Post MapPostRequestDtoToPostModel(CreatePostDto dto, int authorId);
}
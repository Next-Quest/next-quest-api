using NextQuest.DTOs;
using NextQuest.Models;

namespace NextQuest.Interfaces;

public interface IPostInterface
{
    public Task<(bool Success, string Message)> CreatePostAsync(Post post);
    public Post MapDtoToModel(PostDto dto);
}
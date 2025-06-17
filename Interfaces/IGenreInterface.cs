using NextQuest.DTOs.GenreDtos;
using NextQuest.Models;

namespace NextQuest.Interfaces;

public interface IGenreInterface
{
    public Task<(bool Success, string Message)> CreateGenreAsync(Genre genre);
    public (bool Success, string Message, List<GenreDto>? Genres) GetAllGenres();
    public Task<(bool Success, string Message)> UpdateGenreAsync(UpdateGenreDto genreDto);
    public Task<(bool Success, string Message)> DeleteGenreAsync(int genreId);

    #region DtoMapping
    
    public Genre MapCreateGenreDtoToGenreModel(CreateGenreDto genreDto);
    
    #endregion
}
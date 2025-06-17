using Microsoft.IdentityModel.Tokens;
using NextQuest.Data;
using NextQuest.DTOs.GameDtos;
using NextQuest.DTOs.GenreDtos;
using NextQuest.Interfaces;
using NextQuest.Models;

namespace NextQuest.Services;

public class GenreService : IGenreInterface
{
    private readonly AppDbContext _context;

    public GenreService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<(bool Success, string Message)> CreateGenreAsync(Genre genre)
    {
        try
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        
            return (true, "Gênero criado com sucesso.");
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public (bool Success, string Message, List<GenreDto>? Genres) GetAllGenres()
    {
        try
        {
            var response = _context.Genres.AsQueryable();

            var genres = MapGenreModelToGenreDtoList(response.ToList());
            
            if (genres.Count == 0)
            {
                return (false, "Nenhum gênero encontrado.", null);
            }
            
            return (true, "Jogos encontrados com sucesso.", genres);
        }
        catch (Exception e)
        {
            return (false, e.Message, null);
        }
    }

    public async Task<(bool Success, string Message)> UpdateGenreAsync(UpdateGenreDto genreDto)
    {
        try
        {
            var existingGenre = await _context.Genres.FindAsync(genreDto.Id);
            if (existingGenre == null)
            {
                return (false, "Gênero não encontrado.");
            }

            if (genreDto.GenreName.IsNullOrEmpty())
            {
                return (false, "Nenhuma informação a ser atualizada.");
            }

            if (!genreDto.GenreName.IsNullOrEmpty())
            {
                existingGenre.GenreName = genreDto.GenreName;
            }
            
            await _context.SaveChangesAsync();
            return (true, "Gênero atualizado com sucesso.");
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool Success, string Message)> DeleteGenreAsync(int genreId)
    {
        try
        {
            var existingGenre = await _context.Genres.FindAsync(genreId);
            if (existingGenre == null)
            {
                return (false, "Jogo não encontrado.");
            }
            
            _context.Genres.Remove(existingGenre);
            await _context.SaveChangesAsync();
            
            return (true, "Gênero excluído com sucesso.");
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    #region DtoMapping

    private GenreDto MapGenreModelToGenreDto(Genre genre)
    {
        return new GenreDto()
        {
            Id = genre.Id,
            GenreName = genre.GenreName,
        };
    }

    private List<GenreDto> MapGenreModelToGenreDtoList(List<Genre> genres)
    {
        var genreDtoList = new List<GenreDto>();

        foreach (var genre in genres)
        {
            genreDtoList.Add(MapGenreModelToGenreDto(genre));
        }
        
        return genreDtoList;
    }
    
    public Genre MapCreateGenreDtoToGenreModel(CreateGenreDto genreDto)
    {
        return new Genre()
        {
            GenreName = genreDto.GenreName,
        };
    }
    
    #endregion
}
using FlixNet.Application.Domain.Genres.Queries.GetGenres;

namespace FlixNet.Application.Domain.Movies.Queries.GetMovies;

public record MovieDto(
    Guid Id,
    string Title,
    string Description,
    string PosterUrl,
    string VideoUrl,
    GenreDto[] Genres);

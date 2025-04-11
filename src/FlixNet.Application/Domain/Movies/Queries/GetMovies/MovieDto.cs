namespace FlixNet.Application.Domain.Movies.Queries.GetMovies;

public record MovieDto(
    Guid Id,
    string Title,
    string Description,
    string PosterUrl,
    string VideoUrl);

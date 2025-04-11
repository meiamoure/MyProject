namespace FlixNet.Api.Domain.Movies.Request;

public record UpdateMovieRequest(
    Guid Id,
    string Title,
    string Description,
    string Genre,
    string PosterUrl,
    string VideoUrl);

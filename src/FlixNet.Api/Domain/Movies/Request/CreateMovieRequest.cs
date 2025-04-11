namespace FlixNet.Api.Domain.Movies.Request;

public record CreateMovieRequest(
    string Title,
    string Description,
    string Genre,
    string PosterUrl,
    string VideoUrl);
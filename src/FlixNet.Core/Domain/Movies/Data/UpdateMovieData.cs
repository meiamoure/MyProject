namespace FlixNet.Core.Domain.Movies.Data;

public record UpdateMovieData(
    string Title,
    string Description,
    string PosterUrl,
    string VideoUrl);

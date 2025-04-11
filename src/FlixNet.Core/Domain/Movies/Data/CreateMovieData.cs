namespace FlixNet.Core.Domain.Movies.Data;

public record CreateMovieData(
    string Title,
    string Description,
    string PosterUrl,
    string VideoUrl);

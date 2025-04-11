namespace FlixNet.Application.Domain.Movies.Queries.GetMovieDetails;

public record MovieDetailsDto(
Guid Id,
string Title,
string Description,
string VideoUrl,
string PosterUrl);

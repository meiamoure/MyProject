namespace FlixNet.Api.Domain.Genres.Records;

public record UpdateGenreRequest(
    Guid Id,
    string GenreName);

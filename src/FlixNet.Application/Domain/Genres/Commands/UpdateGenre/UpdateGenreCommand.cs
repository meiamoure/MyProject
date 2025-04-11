using MediatR;

namespace FlixNet.Application.Domain.Genres.Commands.UpdateGenre;

public record UpdateGenreCommand(
    Guid Id,
    string GenreName) : IRequest;

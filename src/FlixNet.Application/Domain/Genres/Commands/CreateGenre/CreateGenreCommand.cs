using MediatR;

namespace FlixNet.Application.Domain.Genres.Commands.CreateGenre;

public record CreateGenreCommand(
    string GenreName) : IRequest<Guid>;

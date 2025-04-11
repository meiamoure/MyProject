using MediatR;

namespace FlixNet.Application.Domain.Genres.Commands.DeleteGenre;

public record DeleteGenreCommand(Guid Id) : IRequest;

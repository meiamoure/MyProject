using MediatR;

namespace FlixNet.Application.Domain.Movies.Commands.AssignGenre;

public record AssignGenreCommand(Guid GenreId, Guid MovieId) : IRequest;

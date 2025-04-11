using MediatR;

namespace FlixNet.Application.Domain.Movies.Commands.RemoveGenre;

public record RemoveGenreCommand(Guid MovieId, Guid GenreId) : IRequest;

using MediatR;

namespace FlixNet.Application.Domain.Movies.Commands.CreateMovie;

public record CreateMovieCommand(
    string Title,
    string Description,
    string PosterUrl,
    string VideoUrl) : IRequest<Guid>;

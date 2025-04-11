using MediatR;

namespace FlixNet.Application.Domain.Movies.Commands.UpdateMovie;

public record UpdateMovieCommand(
    Guid Id,
    string Title,
    string Description,
    string PosterUrl,
    string VideoUrl) : IRequest;
using MediatR;

namespace FlixNet.Application.Domain.Movies.Commands.DeleteMovie;

public record DeleteMovieCommand(Guid Id) : IRequest;

using MediatR;

namespace FlixNet.Application.Domain.Movies.Queries.GetMovieDetails;

public record GetMovieDetailsQuery(Guid Id) : IRequest<MovieDetailsDto>;

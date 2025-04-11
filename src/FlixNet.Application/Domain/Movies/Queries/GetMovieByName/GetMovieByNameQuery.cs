using FlixNet.Application.Domain.Movies.Queries.GetMovies;
using MediatR;

namespace FlixNet.Application.Domain.Movies.Queries.GetMovieByName;

public record GetMovieByNameQuery(string Title) : IRequest<IEnumerable<MovieDto>>;

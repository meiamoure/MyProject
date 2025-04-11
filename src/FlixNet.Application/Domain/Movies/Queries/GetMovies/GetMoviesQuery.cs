using FlixNet.Application.Common;
using MediatR;

namespace FlixNet.Application.Domain.Movies.Queries.GetMovies;

public record GetMoviesQuery(
    int Page, 
    int PageSize,
    Guid? GenreId = null) : IRequest<PageResponse<MovieDto[]>>;

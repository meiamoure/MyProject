using FlixNet.Application.Common;
using MediatR;

namespace FlixNet.Application.Domain.Genres.Queries.GetGenres;

public record GetGenresQuery(int Page, int PageSize) : IRequest<PageResponse<GenreDto[]>>;

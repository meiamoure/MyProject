using FlixNet.Application.Common;
using FlixNet.Application.Domain.Genres.Queries.GetGenres;
using FlixNet.Persistence.EFCore.FlixNetDb;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlixNet.Infrastructure.Application.Domain.Genres.Queries.GetGenres;

public class GetGenresQueryHandler(FlixNetDbContext dbContext) :
    IRequestHandler<GetGenresQuery, PageResponse<GenreDto[]>>
{
    public async Task<PageResponse<GenreDto[]>> Handle(
        GetGenresQuery query, CancellationToken cancellationToken)
    {
        var skip = query.PageSize * (query.Page - 1);

        var sqlQuery = dbContext.Genres
            .AsNoTracking()
            .OrderBy(b => b.GenreName);
        var count = await sqlQuery.CountAsync(cancellationToken);

        var genres = await sqlQuery
            .OrderBy(b => b.GenreName)
            .Skip(skip)
            .Take(query.PageSize)
            .Select(x => new GenreDto(
                x.Id,
                x.GenreName
             ))
            .ToArrayAsync(cancellationToken);

        return new PageResponse<GenreDto[]>(count, genres);
    }
}

using FlixNet.Application.Common;
using FlixNet.Application.Domain.Movies.Queries.GetMovies;
using FlixNet.Persistence.EFCore.FlixNetDb;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlixNet.Infrastructure.Application.Domain.Movies.Queries.GetMovies;

public class GetMoviesQueryHandler(FlixNetDbContext dbContext) :
    IRequestHandler<GetMoviesQuery, PageResponse<MovieDto[]>>
{
    public async Task<PageResponse<MovieDto[]>> Handle(
        GetMoviesQuery query, CancellationToken cancellationToken)
    {
        var skip = query.PageSize * (query.Page - 1);

        var sqlQuery = dbContext.Movies
            .AsNoTracking()
            .Include(m => m.Genres) 
            .ThenInclude(g => g.Genre)
            .AsQueryable();

        if (query.GenreId.HasValue)
        {
            sqlQuery = sqlQuery.Where(m => m.Genres.Any(g => g.GenreId == query.GenreId));
        }

        var count = await sqlQuery.CountAsync(cancellationToken);

        var movies = await sqlQuery
            .OrderBy(m => m.Title)
            .Skip(skip)
            .Take(query.PageSize)
            .Select(x => new MovieDto(
                x.Id,
                x.Title,
                x.Description,
                x.PosterUrl,
                x.VideoUrl
            ))
            .ToArrayAsync(cancellationToken);

        return new PageResponse<MovieDto[]>(count, movies);
    }
}
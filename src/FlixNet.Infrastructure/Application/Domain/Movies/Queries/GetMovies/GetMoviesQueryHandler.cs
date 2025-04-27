using FlixNet.Application.Common;
using FlixNet.Application.Domain.Genres.Queries.GetGenres;
using FlixNet.Application.Domain.Movies.Queries.GetMovies;
using FlixNet.Persistence.EFCore.FlixNetDb;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FlixNet.Infrastructure.Application.Domain.Movies.Queries.GetMovies;

public class GetMoviesQueryHandler(FlixNetDbContext dbContext, IHttpContextAccessor httpContextAccessor) :
    IRequestHandler<GetMoviesQuery, PageResponse<MovieDto[]>>
{
    public async Task<PageResponse<MovieDto[]>> Handle(
        GetMoviesQuery query, CancellationToken cancellationToken)
    {
        var skip = query.PageSize * (query.Page - 1);

        var httpContext = httpContextAccessor.HttpContext!;
        var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";

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
                string.IsNullOrEmpty(x.PosterUrl) ? null : $"{baseUrl}{x.PosterUrl}",
                x.VideoUrl,
                x.Genres
                 .Select(mg => new GenreDto(mg.Genre.Id, mg.Genre.GenreName))
                 .ToArray()
            ))
            .ToArrayAsync(cancellationToken);

        return new PageResponse<MovieDto[]>(count, movies);
    }
}
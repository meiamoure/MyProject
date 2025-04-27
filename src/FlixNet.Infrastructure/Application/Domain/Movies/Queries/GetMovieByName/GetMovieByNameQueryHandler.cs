using FlixNet.Application.Domain.Genres.Queries.GetGenres;
using FlixNet.Application.Domain.Movies.Queries.GetMovieByName;
using FlixNet.Application.Domain.Movies.Queries.GetMovies;
using FlixNet.Persistence.EFCore.FlixNetDb;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlixNet.Infrastructure.Application.Domain.Movies.Queries.GetMovieByName;

public class GetMovieByNameQueryHandler(FlixNetDbContext dbContext) :
    IRequestHandler<GetMovieByNameQuery, IEnumerable<MovieDto>>
{
    public async Task<IEnumerable<MovieDto>> Handle(GetMovieByNameQuery query, CancellationToken cancellationToken)
    {
        var movies = await dbContext
            .Movies
            .AsNoTracking()
            .Where(m =>
                EF.Functions.ILike(m.Title, $"%{query.Title}%"))
            .Select(m => new MovieDto(
                m.Id,
                m.Title,
                m.Description,
                m.PosterUrl,
                m.VideoUrl,
                m.Genres
                 .Select(mg => new GenreDto(mg.Genre.Id, mg.Genre.GenreName))
                 .ToArray()
                ))
            .ToListAsync(cancellationToken);

        if (movies.Count == 0)
            throw new ArgumentException($"No movies found by title: {query.Title}");

        return movies;
    }
}

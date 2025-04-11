using FlixNet.Application.Domain.Genres.Queries.GetGenreByName;
using FlixNet.Application.Domain.Genres.Queries.GetGenres;
using FlixNet.Application.Domain.Movies.Queries.GetMovies;
using FlixNet.Persistence.EFCore.FlixNetDb;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlixNet.Infrastructure.Application.Domain.Genres.Queries.GetGenreByName;

public class GetGenreByNameQueryHandler(FlixNetDbContext dbContext) :
    IRequestHandler<GetGenreByNameQuery, GenreDto>
{
    public async Task<GenreDto> Handle(GetGenreByNameQuery query, CancellationToken cancellationToken)
    {
        var genre = await dbContext
        .Genres
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.GenreName == query.GenreName, cancellationToken)
        ?? throw new ArgumentException($"Genre with name: {query.GenreName} was not found.", nameof(query));

        return new GenreDto(
           genre.Id,
           genre.GenreName);
    }
}

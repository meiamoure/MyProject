using FlixNet.Application.Domain.Movies.Queries.GetMovieDetails;
using FlixNet.Persistence.EFCore.FlixNetDb;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlixNet.Infrastructure.Application.Domain.Movies.Queries.GetMovieDetails;

public class GetMovieDetailsQueryHandler(FlixNetDbContext dbContext)
    : IRequestHandler<GetMovieDetailsQuery, MovieDetailsDto>
{
    public async Task<MovieDetailsDto> Handle(
        GetMovieDetailsQuery query, CancellationToken cancellationToken)
    {
        var movie = await dbContext
            .Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken)
            ?? throw new ArgumentException($"Movie with id: {query.Id} was not found.", nameof(query));

        return new MovieDetailsDto(
            movie.Id,
            movie.Title,
            movie.Description,
            movie.PosterUrl,
            movie.VideoUrl);
    }
}

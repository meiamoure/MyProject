using FlixNet.Core.Domain.Movies.Common;
using FlixNet.Core.Domain.Movies.Models;
using FlixNet.Persistence.EFCore.FlixNetDb;
using Microsoft.EntityFrameworkCore;

namespace FlixNet.Infrastructure.Core.Domain.Movies;

public class MovieRepository(FlixNetDbContext dbContext) : IMovieRepository
{
    public async Task<Movie?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext
            .Movies
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new InvalidOperationException("Movie was not found");
    }

    public void Add(Movie movie)
    {
        dbContext.Add(movie);
    }

    public void Delete(Movie movie)
    {
        dbContext.Remove(movie);
    }
}

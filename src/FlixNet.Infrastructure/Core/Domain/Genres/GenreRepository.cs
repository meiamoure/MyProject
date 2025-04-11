using FlixNet.Core.Domain.Genres.Common;
using FlixNet.Core.Domain.Genres.Models;
using FlixNet.Persistence.EFCore.FlixNetDb;
using Microsoft.EntityFrameworkCore;

namespace FlixNet.Infrastructure.Core.Domain.Genres;

public class GenreRepository(FlixNetDbContext dbContext) : IGenreRepository
{
    public async Task<Genre?> GetGenreById(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext
            .Genres
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new InvalidOperationException("Genre was not found");
    }

    public void Add(Genre genre)
    {
        dbContext.Add(genre);
    }

    public void Delete(Genre genre)
    {
        dbContext.Remove(genre);
    }
}

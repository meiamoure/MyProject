using FlixNet.Core.Domain.Genres.Models;

namespace FlixNet.Core.Domain.Genres.Common;

public interface IGenreRepository
{
    Task<Genre> GetGenreById(Guid authorId, CancellationToken cancellationToken);
    void Add(Genre genre);
    void Delete(Genre genre);
}

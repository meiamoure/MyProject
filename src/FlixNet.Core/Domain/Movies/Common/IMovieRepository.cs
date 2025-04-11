using FlixNet.Core.Domain.Movies.Models;

namespace FlixNet.Core.Domain.Movies.Common;

public interface IMovieRepository
{
    Task<Movie> GetById(Guid id, CancellationToken cancellationToken);

    void Add(Movie movie);

    void Delete(Movie movie);
}

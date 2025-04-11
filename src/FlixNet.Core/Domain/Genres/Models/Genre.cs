using FlixNet.Core.Domain.Genres.Data;

namespace FlixNet.Core.Domain.Genres.Models;

public class Genre
{
    private readonly List<MovieGenre> _movies = [];
    private Genre()
    {
    }

    public Guid Id { get; private set; }
    public string GenreName { get; private set; }

    public IReadOnlyCollection<MovieGenre> Movies => _movies.AsReadOnly();

    internal Genre(
        Guid id,
        string genreName)
    {
        Id = id;
        GenreName = genreName;
    }

    public void Update(UpdateGenreData data)
    {
        GenreName = data.GenreName;
    }

    public static Genre Create(CreateGenreData data)
    {
        return new Genre(
            Guid.NewGuid(),
            data.GenreName);
    }
}

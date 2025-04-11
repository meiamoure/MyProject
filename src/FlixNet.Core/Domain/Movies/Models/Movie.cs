using FlixNet.Core.Domain.Genres.Models;
using FlixNet.Core.Domain.Movies.Data;

namespace FlixNet.Core.Domain.Movies.Models;

public class Movie
{
    private readonly List<MovieGenre> _genres = [];

    private Movie()
    {
    }

    private Movie(
        Guid id,
        string title,
        string description,
        string posterUrl,
        string videoUrl)
    {
        Id = id;
        Title = title;
        Description = description;
        PosterUrl = posterUrl;
        VideoUrl = videoUrl;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string PosterUrl { get; set; }
    public string VideoUrl { get; set; }
    public double Rating { get; set; }
  

    public IReadOnlyCollection<MovieGenre> Genres => _genres.AsReadOnly();

    public static Movie Create(CreateMovieData data)
    {
        return new Movie(
            Guid.NewGuid(),
            data.Title,
            data.Description,
            data.PosterUrl,
            data.VideoUrl
        );
    }

    public void Update(UpdateMovieData data)
    {
        Title = data.Title;
        Description = data.Description;
        PosterUrl = data.PosterUrl;
        VideoUrl = data.VideoUrl;
    }

    public void AssignGenre(Genre genre)
    {
        if (_genres.All(x => x.GenreId != genre.Id))
        {
            var ba = MovieGenre.Create(Id, genre.Id);
            _genres.Add(ba);
        }
    }

    public void AssignGenres(Genre[] genre)
    {
        var movieGenres = genre
            .Where(g => _genres.All(x => x.GenreId != g.Id))
            .Select(genre => MovieGenre.Create(Id, genre.Id));

        _genres.AddRange(movieGenres);
    }

    public void RemoveGenre(Genre genre)
    {
        var ra = _genres.FirstOrDefault(x => x.GenreId == genre.Id);
        if (ra is not null)
        {
            _genres.Remove(ra);
        }
    }
}

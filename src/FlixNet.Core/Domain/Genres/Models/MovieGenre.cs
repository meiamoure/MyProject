using FlixNet.Core.Domain.Movies.Models;

namespace FlixNet.Core.Domain.Genres.Models;

public class MovieGenre
{
    private MovieGenre()
    {
    }

    private MovieGenre(Guid movieId, Guid genreId)
    {
        MovieId = movieId;
        GenreId = genreId;
    }
    public Guid MovieId { get; set; }
    public Movie Movie { get; set; }

    public Guid GenreId { get; set; }
    public Genre Genre { get; set; }

    public static MovieGenre Create(Guid movieId, Guid genreId)
    {
        return new MovieGenre(movieId, genreId);
    }
}

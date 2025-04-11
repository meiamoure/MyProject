using FlixNet.Core.Common;
using FlixNet.Core.Domain.Genres.Common;
using FlixNet.Core.Domain.Movies.Common;
using MediatR;

namespace FlixNet.Application.Domain.Movies.Commands.AssignGenre;

public class AssignGenreCommandHandler(
    IGenreRepository genreRepository, IMovieRepository movieRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<AssignGenreCommand>
{
    public async Task Handle(AssignGenreCommand command, CancellationToken cancellationToken)
    {
        var genre = await movieRepository.GetById(command.GenreId, cancellationToken);
        var movie = await genreRepository.GetGenreById(command.MovieId, cancellationToken);
        genre.AssignGenre(movie);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

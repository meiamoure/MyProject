using FlixNet.Core.Common;
using FlixNet.Core.Domain.Genres.Common;
using FlixNet.Core.Domain.Movies.Common;
using MediatR;

namespace FlixNet.Application.Domain.Movies.Commands.RemoveGenre;

public class RemoveGenreCommandHandler(IGenreRepository genreRepository,
    IMovieRepository bookRepository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveGenreCommand>
{
    public async Task Handle(RemoveGenreCommand command, CancellationToken cancellationToken)
    {
        var movie = await bookRepository.GetById(command.MovieId, cancellationToken) ?? throw new Exception("Movie not found");
        var genre = await genreRepository.GetGenreById(command.GenreId, cancellationToken) ?? throw new Exception("Genre not found");
        movie.RemoveGenre(genre);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

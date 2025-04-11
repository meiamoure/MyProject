using FlixNet.Core.Common;
using FlixNet.Core.Domain.Genres.Common;
using MediatR;

namespace FlixNet.Application.Domain.Genres.Commands.DeleteGenre;

public class DeleteGenreCommandHandler(
    IGenreRepository genreRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteGenreCommand>
{
    public async Task Handle(DeleteGenreCommand command, CancellationToken cancellationToken)
    {
        var author = await genreRepository.GetGenreById(command.Id, cancellationToken);
        genreRepository.Delete(author);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

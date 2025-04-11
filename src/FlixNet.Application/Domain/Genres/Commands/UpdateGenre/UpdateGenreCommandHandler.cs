using FlixNet.Core.Common;
using FlixNet.Core.Domain.Genres.Common;
using FlixNet.Core.Domain.Genres.Data;
using MediatR;

namespace FlixNet.Application.Domain.Genres.Commands.UpdateGenre;

public class UpdateAuthorCommandHandler(
    IGenreRepository genreRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateGenreCommand>
{
    public async Task Handle(UpdateGenreCommand command, CancellationToken cancellationToken)
    {
        var author = await genreRepository.GetGenreById(command.Id, cancellationToken);
        var data = new UpdateGenreData(command.GenreName);
        author.Update(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

using FlixNet.Core.Common;
using FlixNet.Core.Domain.Genres.Common;
using FlixNet.Core.Domain.Genres.Data;
using FlixNet.Core.Domain.Genres.Models;
using MediatR;

namespace FlixNet.Application.Domain.Genres.Commands.CreateGenre;

public class CreateGenreCommandHandler(IGenreRepository genreRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateGenreCommand, Guid>
{
    public async Task<Guid> Handle(CreateGenreCommand command, CancellationToken cancellationToken)
    {
        var data = new CreateGenreData(command.GenreName);
        var genre = Genre.Create(data);

        genreRepository.Add(genre);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return genre.Id;
    }
}

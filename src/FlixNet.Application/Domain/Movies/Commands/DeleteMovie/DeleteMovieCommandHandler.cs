using FlixNet.Core.Common;
using FlixNet.Core.Domain.Movies.Common;
using MediatR;

namespace FlixNet.Application.Domain.Movies.Commands.DeleteMovie;

public class DeleteMovieCommandHandler(IMovieRepository movieRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteMovieCommand>
{
    public async Task Handle(
        DeleteMovieCommand command,
        CancellationToken cancellationToken)
    {
        var movie = await movieRepository.GetById(command.Id, cancellationToken);
        movieRepository.Delete(movie);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

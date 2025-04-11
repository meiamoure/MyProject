using FlixNet.Core.Common;
using FlixNet.Core.Domain.Movies.Common;
using FlixNet.Core.Domain.Movies.Data;
using FlixNet.Core.Domain.Movies.Models;
using MediatR;

namespace FlixNet.Application.Domain.Movies.Commands.UpdateMovie;

public class UpdateMovieCommandHandler(
    IMovieRepository movieRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateMovieCommand>
{
    public async Task Handle(
        UpdateMovieCommand command, CancellationToken cancellationToken)
    {
        var movie = await movieRepository.GetById(
            command.Id,
            cancellationToken);

        var data = new UpdateMovieData(
            command.Title,
            command.Description,
            command.PosterUrl,
            command.VideoUrl);
        movie.Update(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

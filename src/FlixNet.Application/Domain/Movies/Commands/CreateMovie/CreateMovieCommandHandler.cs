using FlixNet.Core.Common;
using FlixNet.Core.Domain.Movies.Common;
using FlixNet.Core.Domain.Movies.Data;
using FlixNet.Core.Domain.Movies.Models;
using MediatR;

namespace FlixNet.Application.Domain.Movies.Commands.CreateMovie;

public class CreateMovieCommandHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateMovieCommand, Guid>
{
    public async Task<Guid> Handle(CreateMovieCommand command, CancellationToken cancellationToken)
    {
        var data = new CreateMovieData(command.Title, command.Description, command.PosterUrl, command.VideoUrl);
        var movie = Movie.Create(data);

        movieRepository.Add(movie);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return movie.Id;
    }
}

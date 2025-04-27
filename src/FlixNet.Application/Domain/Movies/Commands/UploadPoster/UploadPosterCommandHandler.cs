using FlixNet.Application.Domain.Movies.Commands.UploadPoster;
using FlixNet.Application.Domain.Movies.Queries.GetMovieByName;
using FlixNet.Core.Common;
using FlixNet.Core.Domain.Movies.Common;
using FlixNet.Core.Domain.Movies.Data;
using MediatR;
public class UploadPosterCommandHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<UploadPosterCommand, UploadPosterResultDto>
{
    public async Task<UploadPosterResultDto> Handle(UploadPosterCommand request, CancellationToken cancellationToken)
    {
        var movie = await movieRepository.GetById(request.Id, cancellationToken);
        if (movie == null || request.File == null || request.File.Length == 0)
            return new UploadPosterResultDto(false, string.Empty);

        var extension = Path.GetExtension(request.File.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";

        Directory.CreateDirectory(request.UploadPath);

        var fullPath = Path.Combine(request.UploadPath, fileName);

        await using var stream = new FileStream(fullPath, FileMode.Create);
        await request.File.CopyToAsync(stream, cancellationToken);

        var posterUrl = $"/posters/{fileName}";

        var updateData = new UpdateMovieData(
            movie.Title,
            movie.Description,
            posterUrl,
            movie.VideoUrl
        );

        movie.Update(updateData);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new UploadPosterResultDto(true, posterUrl);
    }
}



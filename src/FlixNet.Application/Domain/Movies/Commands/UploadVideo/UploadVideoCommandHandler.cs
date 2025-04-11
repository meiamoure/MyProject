using FlixNet.Application.Domain.Movies.Queries.GetMovieByName;
using FlixNet.Core.Common;
using FlixNet.Core.Domain.Movies.Common;
using FlixNet.Core.Domain.Movies.Data;
using MediatR;


namespace FlixNet.Application.Domain.Movies.Commands.UploadVideo;
public class UploadVideoCommandHandler : IRequestHandler<UploadVideoCommand, UploadVideoResultDto>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UploadVideoCommandHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
    {
        _movieRepository = movieRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UploadVideoResultDto> Handle(UploadVideoCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetById(request.Id, cancellationToken);
        if (movie == null || request.File == null || request.File.Length == 0)
            return new UploadVideoResultDto(false, string.Empty);

        Directory.CreateDirectory(request.UploadPath);
        var fileName = request.File.FileName;
        var fullPath = Path.Combine(request.UploadPath, fileName);

        await using var stream = new FileStream(fullPath, FileMode.Create);
        await request.File.CopyToAsync(stream, cancellationToken);

        var videoUrl = $"/videos/{fileName}";

        var updateData = new UpdateMovieData(
            movie.Title,
            movie.Description,
            videoUrl,
            movie.PosterUrl);

        movie.Update(updateData);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UploadVideoResultDto(true, videoUrl);
    }
}


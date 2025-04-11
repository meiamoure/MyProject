using FlixNet.Application.Domain.Movies.Queries.GetMovieByName;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace FlixNet.Application.Domain.Movies.Commands.UploadVideo;

public record UploadVideoCommand(Guid Id, IFormFile File, string UploadPath) : IRequest<UploadVideoResultDto>;

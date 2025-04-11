using FlixNet.Application.Domain.Movies.Queries.GetMovieByName;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FlixNet.Application.Domain.Movies.Commands.UploadPoster;

public record UploadPosterCommand(Guid Id, IFormFile File, string UploadPath) : IRequest<UploadPosterResultDto>;

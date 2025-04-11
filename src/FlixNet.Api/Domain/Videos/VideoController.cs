using FlixNet.Api.Domain.Videos.Request;
using FlixNet.Application.Domain.Movies.Commands.UploadVideo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlixNet.Api.Domain.Videos;

[ApiController]
[Route("api/videos")]
public class VideosController(IMediator mediator, IWebHostEnvironment env) : ControllerBase
{

    [DisableRequestSizeLimit]
    [RequestSizeLimit(2147483648)]
    [HttpPost("{id}/upload-video")]
    public async Task<IActionResult> Upload(
    [FromRoute] Guid id,
    [FromForm] UploadVideoRequest request)
    {
        var uploadPath = Path.Combine(env.WebRootPath, "videos");

        var result = await mediator.Send(new UploadVideoCommand(id, request.File, uploadPath));

        if (!result.Success)
            return BadRequest("Failed to upload video.");

        return Ok(new { videoUrl = result.VideoUrl });
    }
}
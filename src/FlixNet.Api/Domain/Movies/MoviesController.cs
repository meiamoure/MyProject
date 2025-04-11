using FlixNet.Api.Constants;
using FlixNet.Api.Domain.Movies.Request;
using FlixNet.Application.Domain.Movies.Commands.AssignGenre;
using FlixNet.Application.Domain.Movies.Commands.CreateMovie;
using FlixNet.Application.Domain.Movies.Commands.DeleteMovie;
using FlixNet.Application.Domain.Movies.Commands.RemoveGenre;
using FlixNet.Application.Domain.Movies.Commands.UpdateMovie;
using FlixNet.Application.Domain.Movies.Commands.UploadPoster;
using FlixNet.Application.Domain.Movies.Queries.GetMovieByName;
using FlixNet.Application.Domain.Movies.Queries.GetMovieDetails;
using FlixNet.Application.Domain.Movies.Queries.GetMovies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FlixNet.Api.Domain.Movies;

[Route(Routes.Movies)]
[ApiController]
public class MoviesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [HttpGet]
    public async Task<ActionResult> GetMovies(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] Guid? genreId = null,
    CancellationToken cancellationToken = default)
    {
        var query = new GetMoviesQuery(page, pageSize, genreId);
        var movies = await mediator.Send(query, cancellationToken);
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetMovie(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetMovieDetailsQuery(id);
        var movie = await mediator.Send(query, cancellationToken);
        return Ok(new
        {
            movie.Id,
            movie.Title,
            movie.Description,
            posterUrl = movie.PosterUrl,
            videoUrl = movie.VideoUrl
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<ActionResult> AddMovie(
        [FromBody][Required] CreateMovieCommand request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateMovieCommand(
            request.Title,
            request.Description,
            request.PosterUrl,
            request.VideoUrl
            );
        var id = await mediator.Send(command, cancellationToken);
        return Ok(id);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<ActionResult> UpdateMovie(
        [FromRoute] Guid id,
        [FromBody][Required] UpdateMovieRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateMovieCommand(
            id,
            request.Title,
            request.Description,
            request.PosterUrl,
            request.VideoUrl
            );
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteMovie(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteMovieCommand(id);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPost("{id}/add-genre")]
    public async Task<ActionResult> AssignGenre(
        [FromRoute] Guid id,
        [FromBody][Required] Guid authorId,
        CancellationToken cancellationToken = default)
    {
        var command = new AssignGenreCommand(id, authorId);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}/remove-genre")]
    public async Task<ActionResult> RemoveGenre(
        [FromRoute] Guid id,
        [FromBody][Required] Guid authorId,
        CancellationToken cancellationToken = default)
    {
        var command = new RemoveGenreCommand(id, authorId);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpGet("{name}/get-by-name")]
    public async Task<ActionResult> GetMovieByName(
        [FromRoute] string name,
        CancellationToken cancellationToken = default)
    {
        var query = new GetMovieByNameQuery(name);
        var movieByName = await mediator.Send(query, cancellationToken);
        return Ok(movieByName);
    }

    [HttpPost("{id}/upload-poster")]
    [DisableRequestSizeLimit]
    [RequestSizeLimit(20485760)]
    public async Task<IActionResult> UploadPoster(
    [FromRoute] Guid id,
    [FromForm] UploadPosterRequest request,
    [FromServices] IWebHostEnvironment env,
    CancellationToken cancellationToken = default)
    {
        var uploadPath = Path.Combine(env.WebRootPath, "posters");

        var result = await mediator.Send(new UploadPosterCommand(id, request.File, uploadPath), cancellationToken);

        if (!result.Success)
            return BadRequest("Failed to upload poster.");

        return Ok(new { posterUrl = result.PosterUrl });
    }

}
using FlixNet.Api.Constants;
using FlixNet.Api.Domain.Genres.Records;
using FlixNet.Api.Domain.Movies.Request;
using FlixNet.Application.Domain.Genres.Commands.CreateGenre;
using FlixNet.Application.Domain.Genres.Commands.DeleteGenre;
using FlixNet.Application.Domain.Genres.Commands.UpdateGenre;
using FlixNet.Application.Domain.Genres.Queries.GetGenres;
using FlixNet.Application.Domain.Movies.Queries.GetMovieDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FlixNet.Api.Domain.Genres
{
    [Route(Routes.Genres)]
    [ApiController]
    public class GenresController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetGenres(
            [FromQuery][Required] int page = 1,
            [FromQuery][Required] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var query = new GetGenresQuery(page, pageSize);
            var genres = await mediator.Send(query, cancellationToken);
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetGenre(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var query = new GetMovieDetailsQuery(id);
            var movie = await mediator.Send(query, cancellationToken);
            return Ok(movie);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<ActionResult> AddGenre(
            [FromBody][Required] CreateGenreRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = new CreateGenreCommand(
                request.GenreName);
            var id = await mediator.Send(command, cancellationToken);
            return Ok(id);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateGenre(
            [FromRoute] Guid id,
            [FromBody][Required] UpdateGenreRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = new UpdateGenreCommand(
                id,
                request.GenreName);
            await mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteGenre(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteGenreCommand(id);
            await mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}

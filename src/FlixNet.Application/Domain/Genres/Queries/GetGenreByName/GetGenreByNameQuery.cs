using FlixNet.Application.Domain.Genres.Queries.GetGenres;
using MediatR;

namespace FlixNet.Application.Domain.Genres.Queries.GetGenreByName;
public record GetGenreByNameQuery(string GenreName) : IRequest<GenreDto>;

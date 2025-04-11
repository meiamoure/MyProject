using MediatR;

namespace FlixNet.Application.Domain.Genres.Queries.GetGenreDetails;

public record GetGenreDetailsQuery(Guid Id) : IRequest<GenreDetailsDto>;

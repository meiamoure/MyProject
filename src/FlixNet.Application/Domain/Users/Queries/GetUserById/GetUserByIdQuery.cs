using FlixNet.Application.Domain.Users.Commands;
using MediatR;

namespace FlixNet.Application.Domain.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto>;

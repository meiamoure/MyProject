using FlixNet.Application.Domain.Users.Commands;
using MediatR;

namespace FlixNet.Application.Domain.Users.Queries.GetMyProfile;

public record GetMyProfileQuery(Guid UserId) : IRequest<UserDto>;

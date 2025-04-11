using MediatR;

namespace FlixNet.Application.Domain.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) : IRequest<Unit>;

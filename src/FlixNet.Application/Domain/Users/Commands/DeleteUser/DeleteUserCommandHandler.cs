using FlixNet.Core.Common;
using FlixNet.Core.Domain.Users.Common;
using MediatR;

namespace FlixNet.Application.Domain.Users.Commands.DeleteUser;


public class DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        userRepository.Delete(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}


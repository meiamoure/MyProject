using FlixNet.Core.Common;
using FlixNet.Core.Domain.Users.Common;
using FlixNet.Core.Domain.Users.Data;
using MediatR;

namespace FlixNet.Application.Domain.Users.Commands.UpdateProfile;

public class UpdateProfileCommandHandler(IUserRepository userRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateProfileCommand, Unit>
{
    public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(
            request.UserId,
            cancellationToken);

        var data = new UpdateUserData(
            request.Name, 
            request.PictureUrl);

        user.UpdateProfile(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

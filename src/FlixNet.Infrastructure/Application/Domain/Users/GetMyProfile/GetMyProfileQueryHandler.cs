using FlixNet.Application.Domain.Users.Commands;
using FlixNet.Application.Domain.Users.Queries.GetMyProfile;
using FlixNet.Core.Domain.Users.Common;
using MediatR;

namespace FlixNet.Infrastructure.Application.Domain.Users.GetMyProfile;

public class GetMyProfileQueryHandler(IUserRepository userRepository) : IRequestHandler<GetMyProfileQuery, UserDto>
{
    public async Task<UserDto> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken)
                   ?? throw new Exception("User not found");

        return new UserDto(
            user.Id, 
            user.Name, 
            user.Email, 
            user.PictureUrl);
    }
}

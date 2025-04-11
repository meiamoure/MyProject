using FlixNet.Application.Domain.Users.Commands;
using FlixNet.Application.Domain.Users.Queries.GetUserById;
using FlixNet.Core.Domain.Users.Common;
using MediatR;

namespace FlixNet.Infrastructure.Application.Domain.Users.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        return new UserDto(
            user.Id,
            user.Name,
            user.Email,
            user.PictureUrl
        );
    }
}

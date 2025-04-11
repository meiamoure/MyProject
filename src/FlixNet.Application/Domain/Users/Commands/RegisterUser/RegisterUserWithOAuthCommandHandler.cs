using FlixNet.Core.Common;
using FlixNet.Core.Domain.Users.Common;
using FlixNet.Core.Domain.Users.Models;
using MediatR;

namespace FlixNet.Application.Domain.Users.Commands.RegisterUser;

public class RegisterWithOAuthCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IJwtProvider jwtProvider) : IRequestHandler<RegisterWithOAuthCommand, string>
{
    public async Task<string> Handle(RegisterWithOAuthCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            user = User.Create(request.Email, request.Name, request.PictureUrl);
            userRepository.Add(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return jwtProvider.Generate(user);
    }
}
using MediatR;

namespace FlixNet.Application.Domain.Users.Commands.RegisterUser;

public record RegisterWithOAuthCommand(string Email, string Name, string PictureUrl)
    : IRequest<string>;

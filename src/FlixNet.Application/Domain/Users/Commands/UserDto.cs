namespace FlixNet.Application.Domain.Users.Commands;
public record UserDto(
    Guid Id, 
    string Email, 
    string Name, 
    string PictureUrl);


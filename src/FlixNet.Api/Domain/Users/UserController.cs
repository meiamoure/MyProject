using FlixNet.Api.Constants;
using FlixNet.Application.Domain.Users.Commands.UpdateProfile;
using FlixNet.Application.Domain.Users.Queries.GetAllUsers;
using FlixNet.Application.Domain.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlixNet.Api.Domain.Users;

[Route(Routes.Users)]
[ApiController]
public class UsersController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var query = new GetUserByIdQuery(Guid.Parse(userId));
        var user = await mediator.Send(query);

        return Ok(user);
    }

    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand command)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
            return Unauthorized();

        command = command with { UserId = Guid.Parse(userId) };

        await mediator.Send(command);
        return NoContent();
    }
}

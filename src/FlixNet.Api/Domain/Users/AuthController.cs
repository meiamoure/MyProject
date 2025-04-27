using MediatR;
using Microsoft.AspNetCore.Mvc;
using FlixNet.Core.Common;
using FlixNet.Core.Domain.Users.Common;
using System.Text.Json;
using Microsoft.Extensions.Options;
using FlixNet.Infrastructure.Core.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FlixNet.Api.Domain.Users;

[Route("api/auth")]
public class AuthController(
    IMediator mediator,
    IJwtProvider jwtProvider,
    IUserRepository userRepository,
    IOptions<GoogleOAuthSettings> googleOptions,
    IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly GoogleOAuthSettings _googleSettings = googleOptions.Value;

    [HttpGet("signin-google")]
    public IActionResult SignInGoogle()
    {
        var redirectUrl = $"{_googleSettings.AuthEndpoint}?client_id={_googleSettings.ClientId}" +
                          $"&redirect_uri={_googleSettings.RedirectUri}" +
                          $"&response_type=code&scope=openid%20email%20profile";
        return Redirect(redirectUrl);
    }

    [HttpGet("callback/google")]
    public async Task<IActionResult> Callback(string code)
    {
        using var httpClient = new HttpClient();

        var tokenResponse = await httpClient.PostAsync(_googleSettings.TokenEndpoint, new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["code"] = code,
            ["client_id"] = _googleSettings.ClientId,
            ["client_secret"] = _googleSettings.ClientSecret,
            ["redirect_uri"] = _googleSettings.RedirectUri,
            ["grant_type"] = "authorization_code"
        }));

        var tokenJson = await tokenResponse.Content.ReadAsStringAsync();
        var tokenData = JsonDocument.Parse(tokenJson).RootElement;
        var accessToken = tokenData.GetProperty("access_token").GetString();

        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        var userInfoResponse = await httpClient.GetAsync(_googleSettings.UserInfoEndpoint);
        var userInfoJson = await userInfoResponse.Content.ReadAsStringAsync();
        var userInfo = JsonDocument.Parse(userInfoJson).RootElement;

        var email = userInfo.GetProperty("email").GetString()!;
        var name = userInfo.GetProperty("name").GetString()!;
        var picture = userInfo.GetProperty("picture").GetString()!;

        var user = await userRepository.GetByEmailAsync(email);
        if (user == null)
        {
            user = FlixNet.Core.Domain.Users.Models.User.Create(name, email, picture);
            userRepository.Add(user);
            await unitOfWork.SaveChangesAsync();
        }

        var token = jwtProvider.Generate(user);
        var redirectUrl = $"http://localhost:5173/oauth-callback?token={token}";
        return Redirect(redirectUrl);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe(CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized("Invalid token.");

        var user = await userRepository.GetByIdAsync(userId, cancellationToken);

        if (user == null)
            return NotFound("User does not found.");

        return Ok(new
        {
            user.Id,
            user.Name,
            user.Email,
            user.PictureUrl
        });
    }
}

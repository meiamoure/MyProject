using FlixNet.Core.Domain.Users.Models;

namespace FlixNet.Core.Common;

public interface IJwtProvider
{
    string Generate(User user);
}

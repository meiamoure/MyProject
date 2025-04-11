using FlixNet.Core.Domain.Movies.Models;
using FlixNet.Core.Domain.Users.Models;

namespace FlixNet.Core.Domain.Users.Common;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(User user);
    void Update(User user);
    void Delete(User user);
}

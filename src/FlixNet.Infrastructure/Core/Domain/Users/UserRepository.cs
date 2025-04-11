using FlixNet.Core.Domain.Movies.Models;
using FlixNet.Core.Domain.Users.Common;
using FlixNet.Core.Domain.Users.Models;
using FlixNet.Persistence.EFCore.FlixNetDb;
using Microsoft.EntityFrameworkCore;

namespace FlixNet.Infrastructure.Core.Domain.Users;

public class UserRepository(FlixNetDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public void Add(User user)
    {
        dbContext.Add(user);
    }

    public void Update(User user)
    {
        dbContext.Users.Update(user);
    }

    public void Delete(User user)
    {
        dbContext.Remove(user);
    }
}

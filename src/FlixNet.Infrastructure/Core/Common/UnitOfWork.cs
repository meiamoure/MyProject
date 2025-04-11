using FlixNet.Core.Common;
using FlixNet.Persistence.EFCore.FlixNetDb;

namespace FlixNet.Infrastructure.Core.Common;

public class UnitOfWork(FlixNetDbContext dbContext) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}

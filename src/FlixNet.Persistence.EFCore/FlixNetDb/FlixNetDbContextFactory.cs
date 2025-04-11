using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FlixNet.Persistence.EFCore.FlixNetDb;

public class FlixNetDbContextFactory : IDesignTimeDbContextFactory<FlixNetDbContext>
{
    public FlixNetDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<FlixNetDbContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("FlixNet"));

        return new FlixNetDbContext(optionsBuilder.Options);
    }
}

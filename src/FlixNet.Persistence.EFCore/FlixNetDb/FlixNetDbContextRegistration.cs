using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlixNet.Persistence.EFCore.FlixNetDb;

public static class FlixNetDbContextRegistration
{
    public static void RegisterFlixNetDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("FlixNet");

        services.AddDbContext<FlixNetDbContext>(options =>
        {
            options.UseNpgsql(
                connectionString,
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsHistoryTable(
                        FlixNetDbContext.FlixNetMigrationHistory,
                        FlixNetDbContext.FlixNetDbSchema);
                });
        });
    }
}

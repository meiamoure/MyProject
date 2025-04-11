using FlixNet.Core.Domain.Genres.Models;
using FlixNet.Core.Domain.Movies.Models;
using FlixNet.Core.Domain.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixNet.Persistence.EFCore.FlixNetDb;

public class FlixNetDbContext(DbContextOptions<FlixNetDbContext> options) : DbContext(options)
{
    public const string FlixNetDbSchema = "flixnet";

    public const string FlixNetMigrationHistory = "__FlixNetMigrationHistory";

    public DbSet<Movie> Movies { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (env == "Development")
        {
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(FlixNetDbSchema);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FlixNetDbContext).Assembly);
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FlixNet.Core.Domain.Movies.Models;

namespace FlixNet.Persistence.EFCore.FlixNetDb.EntityTypesConfigurations;

internal class MovieTypeConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(5000)
            .IsRequired();

        builder.Property(x => x.VideoUrl)
            .HasMaxLength(1000)
            .IsRequired();

        builder.HasIndex(x => x.VideoUrl)
            .IsUnique();

        builder.Property(x => x.PosterUrl)
            .HasMaxLength(1000)
            .IsRequired();

        builder
            .Metadata
            .FindNavigation(nameof(Movie.Genres))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.Genres)
            .WithOne(x => x.Movie)
            .HasForeignKey(x => x.MovieId);
    }
}

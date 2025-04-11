using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FlixNet.Core.Domain.Genres.Models;

namespace FlixNet.Persistence.EFCore.FlixNetDb.EntityTypesConfigurations;

public class MovieGenreEntityTypeConfiguration : IEntityTypeConfiguration<MovieGenre>
{
    public void Configure(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.HasKey(x => new { x.MovieId, x.GenreId });
    }
}

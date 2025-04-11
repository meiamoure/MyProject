using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FlixNet.Core.Domain.Genres.Models;

namespace FlixNet.Persistence.EFCore.FlixNetDb.EntityTypesConfigurations;

internal class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.GenreName)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Metadata
            .FindNavigation(nameof(Genre.Movies))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.Movies)
            .WithOne(x => x.Genre)
            .HasForeignKey(x => x.GenreId);
    }
}

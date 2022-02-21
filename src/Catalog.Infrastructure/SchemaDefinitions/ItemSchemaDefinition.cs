using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.SchemaDefinitions;

public class ItemSchemaDefinition : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items", CatalogContext.DEFAULT_SCHEMA);
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Name)
            .IsRequired();

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne(g => g.Genre)
            .WithMany(i => i.Items)
            .HasForeignKey(fk => fk.GenreId);
        
        builder.HasOne(a => a.Artist)
            .WithMany(i => i.Items)
            .HasForeignKey(fk => fk.ArtistId);

        builder.Property(p => p.Price).HasConversion(
            p => $"{p.Amount}:{p.Currency}",
            p => new Price
            {
                Amount = Convert.ToDecimal(p.Split(':', StringSplitOptions.None)[0]),
                Currency = p.Split(':', StringSplitOptions.None)[1]
            }
        );
    }
}
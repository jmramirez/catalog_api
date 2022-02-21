using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.SchemaDefinitions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure;

public class CatalogContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "catalog";
    
    public DbSet<Item> Items { get; set; }
    
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ItemSchemaDefinition());
        modelBuilder.ApplyConfiguration(new ArtistSchemaDefinition());
        modelBuilder.ApplyConfiguration(new GenreSchemaDefinition());
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await SaveChangesAsync(cancellationToken);
        return true;
    }
}
using Catalog.Domain.Mappers;
using Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace Catalog.Fixtures
{
    public class CatalogContextFactory
    {
        public readonly TestCatalogContext ContextInstace;
        public readonly IGenreMapper GenreMapper;
        public readonly IArtistMapper ArtistMapper;
        public readonly IItemMapper ItemMapper;

        public CatalogContextFactory()
        {
            var contextOptions = new DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            EnsureCreation(contextOptions);
            ContextInstace = new TestCatalogContext(contextOptions);

            GenreMapper = new GenreMapper();
            ArtistMapper = new ArtistMapper();
            ItemMapper = new ItemMapper(ArtistMapper, GenreMapper);
        }

        public void EnsureCreation(DbContextOptions<CatalogContext> contextOptions)
        {
            using var context = new TestCatalogContext(contextOptions);
            context.Database.EnsureCreated();
        }
    }
}

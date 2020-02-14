using Catalog.Domain.Entities;
using Catalog.Infrastructure.Tests.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infrastructure.Tests
{
    public class TestCatalogContext : CatalogContext
    {
        public TestCatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Seed<Artist>("./Data/artist.json");
            modelBuilder.Seed<Artist>("./Data/genre.json");
            modelBuilder.Seed<Artist>("./Data/item.json");
        }
    }
}

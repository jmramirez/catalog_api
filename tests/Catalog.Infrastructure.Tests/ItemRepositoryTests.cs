using System;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Catalog.Infrastructure.Tests;

public class ItemRepositoryTests
{
    [Fact]
    public async Task get_should_return_data()
    {
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "get_should_return_data")
            .Options;

        await using var context = new TestCatalogContext(options);
        context.Database.EnsureCreated();

        var sut = new ItemRepository(context);
        var result = await sut.GetAsync();
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task get_should_return_null_with_no_id()
    {
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "get_should_return_null_with_no_id")
            .Options;

        await using var context = new TestCatalogContext(options);
        context.Database.EnsureCreated();

        var sut = new ItemRepository(context);
        var result = await sut.GetAsync(Guid.NewGuid());
        result.ShouldBeNull();
    }
    
    [Theory]
    [InlineData("b5b05534-9263-448c-a69e-0bbd8b3eb90e")]
    public async Task get_should_return_item_by_id(string guid)
    {
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "get_should_return_item_by_id")
            .Options;

        await using var context = new TestCatalogContext(options);
        context.Database.EnsureCreated();

        var sut = new ItemRepository(context);
        var result = await sut.GetAsync(new Guid(guid));
        result.Id.ShouldBe(new Guid(guid));
    }
    
    [Fact]
    public async Task item_should_be_added()
    {
        var testItem = new Item
        {
            Name = "Test Album",
            Description = "Description",
            LabelName = "Label Name",
            Price = new Price{ Amount = 15, Currency = "CAD"},
            PictureUri = "http://localhost:test/image1",
            ReleaseDate = DateTimeOffset.Now,
            AvailableStock = 6,
            GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
            ArtistId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab")
        };
        
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "item_should_be_added")
            .Options;

        await using var context = new TestCatalogContext(options);
        context.Database.EnsureCreated();

        var sut = new ItemRepository(context);
        sut.Add(testItem);
        await sut.UnitOfWork.SaveEntitiesAsync();
        
        context.Items
            .FirstOrDefault(_ => _.Id == testItem.Id).ShouldNotBeNull();
    }
    
    [Fact]
    public async Task item_should_be_updated()
    {
        var testItem = new Item
        {
            Id = new Guid("b5b05534-9263-448c-a69e-0bbd8b3eb90e"),
            Name = "Test Album",
            Description = "Description Updated",
            LabelName = "Label Name",
            Price = new Price{ Amount = 15, Currency = "CAD"},
            PictureUri = "http://localhost:test/image1",
            ReleaseDate = DateTimeOffset.Now,
            AvailableStock = 6,
            GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
            ArtistId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab")
        };
        
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "item_should_be_updated")
            .Options;

        await using var context = new TestCatalogContext(options);
        context.Database.EnsureCreated();

        var sut = new ItemRepository(context);
        sut.Update(testItem);
        await sut.UnitOfWork.SaveEntitiesAsync();
        
        context.Items
            .FirstOrDefault(_ => _.Id == testItem.Id)
            ?.Description.ShouldBe("Description Updated");
    }
}
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


// Testing all the methods of the ItemRepository class
namespace Catalog.Infrastructure.Tests
{
    public class ItemRepositoryTests
    {

        // Testing GetAsync 
        [Fact]
        public async Task should_get_data()
        {
            var options = new DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestCatalogContext(options);
            context.Database.EnsureCreated();

            var itemRepo = new ItemRepository(context);
            var result = await itemRepo.GetAsync();

            result.ShouldNotBeNull();
        }

        // Testing method GetAsync(Guid id) gives error when Id is not present
        [Fact]
        public async Task should_return_null_with_id_not_present()
        {
            var options = new DbContextOptionsBuilder<CatalogContext>()
                 .UseInMemoryDatabase(databaseName: "should_return_null_with_id_not_present")
                 .Options;

            await using var context = new TestCatalogContext(options);
            context.Database.EnsureCreated();

            var itemRepo = new ItemRepository(context);
            var result = await itemRepo.GetAsync(Guid.NewGuid());

            result.ShouldBeNull();
        }

        // Testing method GetAsync(Guid id) returns item with Id
        [Theory]
        [InlineData("b5b05534-9263-448c-a69e-0bbd8b3eb90e")]
        public async Task should_return_record_by_id(string guid)
        {
            var options = new DbContextOptionsBuilder<CatalogContext>()
                 .UseInMemoryDatabase(databaseName: "should_return_record_by_id")
                 .Options;

            await using var context = new TestCatalogContext(options);
            context.Database.EnsureCreated();

            var itemRepo = new ItemRepository(context);
            var result = await itemRepo.GetAsync(new Guid(guid));

            result.Id.ShouldBe(new Guid(guid));
        }

        //Test method Add
        [Fact]
        public async Task should_add_new_item()
        {

            var testItem = new Item
            {
                Name = "Test Album",
                Description = "Description",
                LabelName = "Label Name",
                Price = new Price { Amount = 13, Currency = "CAD" },
                PictureUri = "https://mycdn.com/pictures/32423423",
                ReleaseData = DateTimeOffset.Now,
                AvailableStock = 6,
                GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
                ArtistId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab")
            };

            var options = new DbContextOptionsBuilder<CatalogContext>()
                 .UseInMemoryDatabase(databaseName: "should_add_new_item")
                 .Options;

            await using var context = new TestCatalogContext(options);
            context.Database.EnsureCreated();

            var itemRepo = new ItemRepository(context);
            itemRepo.Add(testItem);

            await itemRepo.UnitOfWork.SaveEntitiesAsync();

            context.Items.FirstOrDefaultAsync(_ => _.Id == testItem.Id).ShouldNotBeNull();
        }

        //Testing method Update
        [Fact]
        public async Task should_update_item()
        {
            var testItem = new Item
            {
                Id = new Guid("b5b05534-9263-448c-a69e-0bbd8b3eb90e"),
                Name = "Test album",
                Description = "Description updated",
                LabelName = "Label name",
                Price = new Price { Amount = 50, Currency = "EUR" },
                PictureUri = "https://mycdn.com/pictures/32423423",
                ReleaseData = DateTimeOffset.Now,
                AvailableStock = 6,
                GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
                ArtistId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab")
            };

            var options = new DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase("should_update_item").Options;
            await using var context = new TestCatalogContext(options);
            context.Database.EnsureCreated();

            var itemRepo = new ItemRepository(context);
            itemRepo.Update(testItem);

            await itemRepo.UnitOfWork.SaveEntitiesAsync();

            context.Items.FirstOrDefault(x => x.Id == testItem.Id)
                ?.Description.ShouldBe("Description updated");
        }

    }
}

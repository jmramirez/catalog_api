using Catalog.Domain.Entities;
using Catalog.Fixtures;
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
    public class ItemRepositoryTests : IClassFixture<CatalogContextFactory>
    {
        private readonly ItemRepository _sut;
        private readonly TestCatalogContext _context;

        public ItemRepositoryTests(CatalogContextFactory catalogContextFactory)
        {
            _context = catalogContextFactory.ContextInstace;
            _sut = new ItemRepository(_context);
        }

        // Testing GetAsync 
        [Fact]
        public async Task should_get_data()
        {
            var result = await _sut.GetAsync();
            result.ShouldNotBeNull();
        }

        // Testing method GetAsync(Guid id) gives error when Id is not present
        [Fact]
        public async Task should_return_null_with_id_not_present()
        {
            var result = await _sut.GetAsync(Guid.NewGuid());
            result.ShouldBeNull();
        }

        // Testing method GetAsync(Guid id) returns item with Id
        [Theory]
        [InlineData("b5b05534-9263-448c-a69e-0bbd8b3eb90e")]
        public async Task should_return_record_by_id(string guid)
        {
            var result = await _sut.GetAsync(new Guid(guid));

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

            _sut.Add(testItem);
            await _sut.UnitOfWork.SaveEntitiesAsync();

            _context.Items.FirstOrDefaultAsync(_ => _.Id == testItem.Id).ShouldNotBeNull();
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

            _sut.Update(testItem);

            await _sut.UnitOfWork.SaveEntitiesAsync();

            var result = _context.Items.FirstOrDefault(x => x.Id == testItem.Id);

            result.Description.ShouldBe("Description updated");
            result.Price.Amount.ShouldBe(50);
        }

        [Theory]
        [InlineData("f5da5ce4-091e-492e-a70a-22b073d75a52")]
        public async Task getitems_should_not_return_inactive_records(string id)
        {
            var result =
                await _sut.GetAsync();

            result.Any(x => x.Id == new Guid(id)).ShouldBeFalse();
        }

    }
}

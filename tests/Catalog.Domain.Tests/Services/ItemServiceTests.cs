using System;
using System.Threading.Tasks;
using Catalog.Domain.Entities;
using Catalog.Domain.Mappers;
using Catalog.Domain.Repositories;
using Catalog.Domain.Requests.Item;
using Catalog.Domain.Services;
using Catalog.Fixtures;
using Catalog.Infrastructure.Repositories;
using Shouldly;
using Xunit;

namespace Catalog.Domain.Tests.Services;

public class ItemServiceTests : IClassFixture<CatalogContextFactory>
{
    private readonly IItemRepository _itemRepository;
    private readonly IItemMapper _mapper;

    public ItemServiceTests(CatalogContextFactory catalogContextFactory)
    {
        _itemRepository = new ItemRepository(catalogContextFactory.ContextInstance);
        _mapper = catalogContextFactory.ItemMapper;
    }

    [Fact]
    public async Task get_items_should_return_data()
    {
        ItemService sut = new ItemService(_itemRepository, _mapper);
        var result = await sut.GetItemsAsync();
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void get_item_should_throw_error_with_null_id()
    {
        ItemService sut = new ItemService(_itemRepository, _mapper);
        sut.GetItemAsync(null).ShouldThrow<ArgumentNullException>();
    }
    
    [Theory]
    [InlineData("b5b05534-9263-448c-a69e-0bbd8b3eb90e")]
    public async Task get_item_should_return_data(string guid)
    {
        ItemService sut = new ItemService(_itemRepository, _mapper);
        var result = await sut.GetItemAsync(new GetItemRequest{ Id = new Guid(guid)});
        result.Id.ShouldBe(new Guid(guid));
    }

    [Fact]
    public async Task service_should_add_item()
    {
        var testItem = new AddItemRequest
        {
            Name = "Test Album",
            Description = "Description",
            LabelName = "Label Name",
            Price = new Price {Amount = 13, Currency = "CAD"},
            PictureUri = "http://localhost:test/image1",
            ReleaseDate = DateTimeOffset.Now,
            AvailableStock = 6,
            GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
            ArtistId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab")
        };

        IItemService sut = new ItemService(_itemRepository, _mapper);
        var result = await sut.AddItemAsync(testItem);
        
        result.Name.ShouldBe(testItem.Name);
        result.Description.ShouldBe(testItem.Description);
        result.GenreId.ShouldBe(testItem.GenreId);
        result.ArtistId.ShouldBe(testItem.ArtistId);
        result.Price.Amount.ShouldBe(testItem.Price.Amount);
        result.Price.Currency.ShouldBe(testItem.Price.Currency);
    }

    [Fact]
    public async Task service_should_edit_item()
    {
        var testItem = new EditItemRequest
        {
            Id = new Guid("b5b05534-9263-448c-a69e-0bbd8b3eb90e"),
            Name = "Test Album",
            Description = "Description Updated",
            LabelName = "Label Name",
            Price = new Price {Amount = 15, Currency = "CAD"},
            PictureUri = "http://localhost:test/image1",
            ReleaseDate = DateTimeOffset.Now,
            AvailableStock = 6,
            GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
            ArtistId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab")
        };

        IItemService sut = new ItemService(_itemRepository, _mapper);
        var result = await sut.EditItemAsync(testItem);
        
        result.Name.ShouldBe(testItem.Name);
        result.Description.ShouldBe(testItem.Description);
        result.GenreId.ShouldBe(testItem.GenreId);
        result.ArtistId.ShouldBe(testItem.ArtistId);
        result.Price.Amount.ShouldBe(testItem.Price.Amount);
        result.Price.Currency.ShouldBe(testItem.Price.Currency);
    }
}
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Catalog.Domain.Entities;
using Catalog.Domain.Requests.Item;
using Catalog.Fixtures;
using Newtonsoft.Json;
using Xunit;
using Shouldly;
namespace Catalog.API.Tests;

public class ItemControllerTests : IClassFixture<InMemoryApplicationFactory<Program>>
{
    private readonly InMemoryApplicationFactory<Program> _factory;
    public ItemControllerTests(InMemoryApplicationFactory<Program> 
        factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/api/items")]
    public async Task get_should_return_success(string url)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task get_by_id_should_return_item()
    {
        const string id = "86bff4f7-05a7-46b6-ba73-d43e2c45840f";
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/api/items/{id}");

        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseEntity = JsonConvert.DeserializeObject<Item>(responseContent);

        responseEntity.ShouldNotBeNull();
    }

    [Fact]
    public async Task add_should_create_new_record()
    {
        var request = new AddItemRequest
        {
            Name = "Test album",
            Description = "Description",
            LabelName = "Label name",
            Price = new Price {Amount = 13, Currency = "EUR"},
            PictureUri = "https://mycdn.com/pictures/32423423",
            ReleaseDate = DateTimeOffset.Now,
            AvailableStock = 6,
            GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
            ArtistId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab")
        };

        var client = _factory.CreateClient();
        var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"/api/items", httpContent);

        response.EnsureSuccessStatusCode();
        response.Headers.Location.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task edit_should_modify_right_item()
    {
        var request = new EditItemRequest
        {
            Id   = new Guid("b5b05534-9263-448c-a69e-0bbd8b3eb90e"),
            Name = "Test album",
            Description = "Description Updated",
            LabelName = "Label name",
            Price = new Price {Amount = 13, Currency = "EUR"},
            PictureUri = "https://mycdn.com/pictures/32423423",
            ReleaseDate = DateTimeOffset.Now,
            AvailableStock = 6,
            GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
            ArtistId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab")
        };

        var client = _factory.CreateClient();
        var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"/api/items/{request.Id}", httpContent);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseEntity = JsonConvert.DeserializeObject<Item>(responseContent);
        
        responseEntity.Name.ShouldBe(request.Name);
        responseEntity.Description.ShouldBe(request.Description);
        responseEntity.GenreId.ShouldBe(request.GenreId);
        responseEntity.ArtistId.ShouldBe(request.ArtistId);
    }
    
}
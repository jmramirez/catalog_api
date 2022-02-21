using Catalog.Domain.Entities;

namespace Catalog.Domain.Requests.Item;

public class AddItemRequest
{
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string LabelName { get; set; } = String.Empty;
    public Price Price { get; set; }
    public string PictureUri { get; set; } = String.Empty;
    public DateTimeOffset ReleaseDate { get; set; }
    public string Format { get; set; } = String.Empty;
    public int AvailableStock { get; set; }
    public Guid GenreId { get; set; }
    public Guid ArtistId { get; set; }
}
namespace Catalog.Domain.Entities;

public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string LabelName { get; set; } = String.Empty;
    public Price Price { get; set; }
    public string PictureUri { get; set; } = String.Empty;
    public DateTimeOffset ReleaseDate { get; set; }
    public string Format { get; set; } = String.Empty;
    public int AvailableStock { get; set; }
    public Guid GenreId { get; set; }
    public Genre Genre { get; set; }
    public Guid ArtistId { get; set; }
    public Artist Artist { get; set; }
}
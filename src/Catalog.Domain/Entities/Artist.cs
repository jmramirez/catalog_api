namespace Catalog.Domain.Entities;

public class Artist
{
    public Guid ArtistId { get; set; }
    public string ArtistName { get; set; } = String.Empty;
    public ICollection<Item> Items { get; set; }
}
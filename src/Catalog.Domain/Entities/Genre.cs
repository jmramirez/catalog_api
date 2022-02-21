namespace Catalog.Domain.Entities;

public class Genre
{
    public Guid GenreId { get; set; }
    public string GenreDescription { get; set; } = String.Empty;
    public ICollection<Item> Items { get; set; }
}
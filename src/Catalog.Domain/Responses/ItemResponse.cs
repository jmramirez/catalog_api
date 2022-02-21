namespace Catalog.Domain.Responses;

public class ItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string LabelName { get; set; } = String.Empty;
    public PriceResponse Price { get; set; }
    public string PictureUri { get; set; } = String.Empty;
    public DateTimeOffset ReleaseDate { get; set; }
    public string Format { get; set; } = String.Empty;
    public int AvailableStock { get; set; }
    public Guid GenreId { get; set; }
    public GenreResponse Genre { get; set; }
    public Guid ArtistId { get; set; }
    public ArtistReponse Artist { get; set; }
}
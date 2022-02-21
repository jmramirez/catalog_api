using Catalog.Domain.Entities;
using Catalog.Domain.Responses;

namespace Catalog.Domain.Mappers;

public interface IArtistMapper
{
    ArtistReponse Map(Artist artist);
}

public class ArtistMapper : IArtistMapper
{
    public ArtistReponse Map(Artist artist)
    {
        if (artist == null) return null;

        return new ArtistReponse
        {
            ArtistId = artist.ArtistId,
            ArtistName = artist.ArtistName
        };
    }
}
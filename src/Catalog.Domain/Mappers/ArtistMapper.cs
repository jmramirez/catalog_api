using Catalog.Domain.Entities;
using Catalog.Domain.Requests.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Mappers
{
    public class ArtistMapper : IArtistMapper
    {
        public ArtistResponse Map(Artist artist)
        {
            if (artist == null) return null;

            return new ArtistResponse { ArtistId = artist.ArtistId, ArtistName = artist.ArtistName };
        }
    }
}

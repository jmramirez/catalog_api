using Catalog.Domain.Entities;
using Catalog.Domain.Requests.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Mappers
{
    public interface IArtistMapper
    {
        ArtistResponse Map(Artist artist);
    }
}

using Catalog.Domain.Entities;
using Catalog.Domain.Requests.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Mappers
{
    public class GenreMapper : IGenreMapper
    {
        public GenreResponse Map(Genre genre)
        {
            if (genre == null) return null;

            return new GenreResponse { GenreId= genre.GenreId, GenreDescription = genre.GenreDescription};
        }
    }
}

using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly CatalogContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public GenreRepository(CatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Genre Add(Genre item)
        {
            return _context.Genres.Add(item).Entity;
        }

        public async Task<IEnumerable<Genre>> GetAsync()
        {
            return await _context.Genres.AsNoTracking().ToListAsync();
        }

        public async Task<Genre> GetAsync(Guid id)
        {
            var item = await _context.Genres.FindAsync(id);

            if (item == null) return null;

            _context.Entry(item).State = EntityState.Detached;
            return item;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using EventWeb.Core.Models;
using EventWeb.Core.Repositories;

namespace EventWeb.Persistence.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

    }
}
using System.Collections.Generic;
using EventWeb.Models;

namespace EventWeb.Repositories
{
    public interface IGenreRepository
    {
        List<Genre> GetGenres();
    }
}
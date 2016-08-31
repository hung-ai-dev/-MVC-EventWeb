using System.Collections.Generic;
using EventWeb.Core.Models;

namespace EventWeb.Core.Repositories
{
    public interface IGenreRepository
    {
        List<Genre> GetGenres();
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventWeb.Models;

namespace EventWeb.Repositories
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
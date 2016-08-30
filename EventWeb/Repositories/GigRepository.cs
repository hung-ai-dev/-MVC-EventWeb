using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EventWeb.Models;
using EventWeb.ViewModels;

namespace EventWeb.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Gig> GetGigAttendance(string userId)
        {
            return _context.Attendances.Where(u => u.AttendeeId == userId).Select(g => g.Gig)
                .Include(g => g.Artist).Include(g => g.Genre).ToList();
        }

        public Gig GetGigWithAttendance(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGigWithArtistAndGenre(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGig(int gigId)
        {
            return _context.Gigs.Single(g => g.Id == gigId);
        }

        public IQueryable<Gig> GetUpcomingGig()
        {
            return _context.Gigs.Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }

        public List<Gig> GetMineGigs(string userId)
        {
            return _context.Gigs.Where(u => u.ArtistId == userId && u.DateTime > DateTime.Now && !u.IsCanceled)
                .Include(g => g.Artist)
                .Include(g => g.Genre).ToList();
        }

    }
}
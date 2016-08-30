using System.Collections.Generic;
using System.Linq;
using EventWeb.Models;

namespace EventWeb.Repositories
{
    public interface IGigRepository
    {
        List<Gig> GetGigAttendance(string userId);
        Gig GetGigWithAttendance(int gigId);
        Gig GetGigWithArtistAndGenre(int gigId);
        Gig GetGig(int gigId);
        IQueryable<Gig> GetUpcomingGig();
        void Add(Gig gig);
        List<Gig> GetMineGigs(string userId);
    }
}
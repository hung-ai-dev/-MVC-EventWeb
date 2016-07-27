using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using EventWeb.Dtos;
using EventWeb.Models;
using Microsoft.AspNet.Identity;

namespace EventWeb.Controllers.API
{
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(GigDto gigDto)
        {
            Debug.WriteLine(gigDto.GigId);
            var userId = User.Identity.GetUserId();

            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == gigDto.GigId && g.ArtistId == userId);

            if (gig.IsCanceled)
                return NotFound();

            gig.Cancel();

            _context.SaveChanges();
            return Ok();
        }
    }
}

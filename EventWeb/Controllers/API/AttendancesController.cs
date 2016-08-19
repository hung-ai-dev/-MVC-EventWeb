using System.Linq;
using System.Web.Http;
using EventWeb.Dtos;
using EventWeb.Models;
using Microsoft.AspNet.Identity;

namespace EventWeb.Controllers.API
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(int id)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == id))
                return BadRequest("The attendance already exists");

            var attendance = new Attendance()
            {
                GigId = id,
                AttendeeId = User.Identity.GetUserId()
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttend(int id)
        {
            var userId = User.Identity.GetUserId();

            var att = _context.Attendances.SingleOrDefault(a => a.AttendeeId == userId && a.GigId == id);
            if (att == null)
                return NotFound();
            _context.Attendances.Remove(att);
            _context.SaveChanges();
            return Ok(id);
        }
    }
}

using System;
using System.Data.Entity;
using EventWeb.Models;
using EventWeb.ViewModels;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace EventWeb.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances.Where(u => u.AttendeeId == userId).Select(g => g.Gig)
                .Include(g => g.Artist).Include(g => g.Genre).ToList();
            var viewModel = new GigsViewModel()
            {
                UpComingGigs = gigs,
                ShowData = User.Identity.IsAuthenticated,
                Heading = "Going"
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var followees = _context.Followings.Where(u => u.FollowerId == userId).Select(g => g.Followee).ToList();
            var viewModel = new FollowingsViewModel()
            {
                Followees = followees,
                ShowData = User.Identity.IsAuthenticated,
                Heading = "Following"
            };

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            GigFormViewModel viewModel = new GigFormViewModel();
            viewModel.Genres = _context.Genres.ToList();
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
            }
                
            var gig = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue,
                DateTime = viewModel.GetDateTime()
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
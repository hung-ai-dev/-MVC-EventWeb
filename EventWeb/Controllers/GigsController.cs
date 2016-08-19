using System;
using System.Data.Entity;
using System.Diagnostics;
using EventWeb.Models;
using EventWeb.ViewModels;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Globalization;
using Microsoft.ApplicationInsights.Web;

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
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs.Where(u => u.ArtistId == userId && u.DateTime > DateTime.Now && !u.IsCanceled)
                    .Include(g => g.Artist)
                    .Include(g => g.Genre).ToList();
            
            return View(gigs);
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
            GigFormViewModel viewModel = new GigFormViewModel()
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Event"
            };
           
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
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
            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.ArtistId == userId && g.Id == id);

            var viewModel = new GigFormViewModel()
            {
                Id = gig.Id,
                Genres = _context.Genres.ToList(),
                Venue = gig.Venue,
                Date = gig.DateTime.ToString("d/MMM/yyyy", CultureInfo.CreateSpecificCulture("en-us")),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Heading = "Edit your event"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            Debug.WriteLine(viewModel.SearchKey);
            return RedirectToAction("Index", "Home", new {query = viewModel.SearchKey});
        }

        public ActionResult Details(int id)
        {
            var gig = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);
            if (gig == null)
                return HttpNotFound();
            var userId = User.Identity.GetUserId();
            var details = new GigDetailsViewModel()
            {
                Gig = gig,
                IsAttending = _context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == id),
                IsFollowing = _context.Followings.Any(a => a.FolloweeId == gig.ArtistId && a.FollowerId == userId)
            };
            return View(details);
        }
    }
}
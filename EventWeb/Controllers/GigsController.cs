using System;
using System.Data.Entity;
using System.Diagnostics;
using EventWeb.Models;
using EventWeb.ViewModels;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Globalization;

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
            foreach (var item in gigs)
                Debug.WriteLine(item.Id);
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
            var gig = _context.Gigs.Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Venue = viewModel.Venue;
            gig.DateTime = viewModel.GetDateTime();
            gig.GenreId = viewModel.Genre;

            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using EventWeb.Models;
using EventWeb.ViewModels;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Globalization;
using EventWeb.Persistence;
using EventWeb.Repositories;
using Microsoft.ApplicationInsights.Web;

namespace EventWeb.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly GigRepository _gigRepository;
        private readonly AttendanceRepository _attendanceRepository;
        private readonly FollowingRepository _followingRepository;
        private readonly UnitOfWork _unitOfWork;

        public GigsController()
        {
            _context = new ApplicationDbContext();
            _gigRepository = new GigRepository(_context);
            _attendanceRepository = new AttendanceRepository(_context);
            _followingRepository = new FollowingRepository(_context);
            _unitOfWork = new UnitOfWork(_context);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel()
            {
                UpComingGigs = _gigRepository.GetGigAttendance(userId),
                ShowData = User.Identity.IsAuthenticated,
                Heading = "Going",
                AttendanceLookup = _attendanceRepository.GetFutureAttendance(userId).ToLookup(a => a.GigId)
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
            _unitOfWork.Complete();
            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var gig = _gigRepository.GetGig(id);

            if (gig == null)
                return HttpNotFound();
            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

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

            var gig = _gigRepository.GetGigWithAttendance(viewModel.Id);

            if (gig == null)
                return HttpNotFound();
            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _unitOfWork.Complete();
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
            var gig = _gigRepository.GetGigWithArtistAndGenre(id);
            if (gig == null)
                return HttpNotFound();
            var userId = User.Identity.GetUserId();
            var details = new GigDetailsViewModel()
            {
                Gig = gig,
                IsAttending = _attendanceRepository.GetAttendance(id, userId) != null,
                IsFollowing = _followingRepository.GetFollowing(gig.ArtistId, userId) != null
            };
            return View(details);
        }
    }
}
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
        private readonly IUnitOfWork _unitOfWork;

        public GigsController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel()
            {
                UpComingGigs = _unitOfWork.GigRepository.GetGigAttendance(userId),
                ShowData = User.Identity.IsAuthenticated,
                Heading = "Going",
                AttendanceLookup = _unitOfWork.AttendanceRepository.GetFutureAttendance(userId).ToLookup(a => a.GigId)
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.GigRepository.GetMineGigs(userId);
            
            return View(gigs);
        }

        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var followees = _unitOfWork.FollowingRepository.GetMineFollowing(userId);
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
                Genres = _unitOfWork.GenreRepository.GetGenres(),
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
                viewModel.Genres = _unitOfWork.GenreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue,
                DateTime = viewModel.GetDateTime()
            };

            _unitOfWork.GigRepository.Add(gig);
            _unitOfWork.Complete();
            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var gig = _unitOfWork.GigRepository.GetGig(id);

            if (gig == null)
                return HttpNotFound();
            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = new GigFormViewModel()
            {
                Id = gig.Id,
                Genres = _unitOfWork.GenreRepository.GetGenres(),
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
                viewModel.Genres = _unitOfWork.GenreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = _unitOfWork.GigRepository.GetGigWithAttendance(viewModel.Id);

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
            var gig = _unitOfWork.GigRepository.GetGigWithArtistAndGenre(id);
            if (gig == null)
                return HttpNotFound();

            var userId = User.Identity.GetUserId();
            var details = new GigDetailsViewModel()
            {
                Gig = gig,
                IsAttending = _unitOfWork.AttendanceRepository.GetAttendance(id, userId),
                IsFollowing = _unitOfWork.FollowingRepository.GetFollowing(gig.ArtistId, userId)
            };
            return View(details);
        }
    }
}
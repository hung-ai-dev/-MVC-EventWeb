using System;
using System.Linq;
using System.Web.Mvc;
using EventWeb.Models;
using EventWeb.Persistence;
using EventWeb.ViewModels;
using Microsoft.AspNet.Identity;

namespace EventWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _context = new ApplicationDbContext();
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGig = _unitOfWork.GigRepository.GetUpcomingGig();

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGig = upcomingGig.Where(g => g.Artist.Name.Contains(query) ||
                                                    g.Venue.Contains(query) ||
                                                    g.Genre.Name.Contains(query));   
            };

            var userId = User.Identity.GetUserId();
            var attendances = _unitOfWork.AttendanceRepository.GetFutureAttendance(userId).ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel()
            {
                UpComingGigs = upcomingGig,
                ShowData = User.Identity.IsAuthenticated,
                Heading = "Home",
                AttendanceLookup = attendances
            };

            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Details(int id)
        {
            var gig = _context.Gigs.Where(g => g.Id == id);
            return View(gig);
        }
    }
}
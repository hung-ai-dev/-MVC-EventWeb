using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventWeb.Models;
using EventWeb.ViewModels;

namespace EventWeb.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(string query = null)
        {
            Debug.WriteLine("Hello2");         

            var upcomingGig = _context.Gigs.Include(g => g.Artist)
                                            .Include(g => g.Genre)
                                            .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);
            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGig = upcomingGig.Where(g => g.Artist.Name.Contains(query) ||
                                                    g.Venue.Contains(query) ||
                                                    g.Genre.Name.Contains(query));   
            };

            var viewModel = new GigsViewModel()
            {
                UpComingGigs = upcomingGig,
                ShowData = User.Identity.IsAuthenticated,
                Heading = "Home"
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
    }
}
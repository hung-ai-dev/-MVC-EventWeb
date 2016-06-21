using EventWeb.Models;
using EventWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventWeb.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Gigs
        public ActionResult Create()
        {
            GigFormViewModel viewModel = new GigFormViewModel();
            viewModel.Genres = _context.Genres.ToList();
            return View(viewModel);
        }
    }
}
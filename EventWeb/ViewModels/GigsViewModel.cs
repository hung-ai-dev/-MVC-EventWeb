using System.Collections.Generic;
using System.Linq;
using EventWeb.Models;

namespace EventWeb.ViewModels
{
    public class GigsViewModel
    {
        public GigsViewModel()
        {
        }

        public IEnumerable<Gig> UpComingGigs { get; set; }
        public bool ShowData { get; set; }
        public string Heading { get; set; }
        public string SearchKey { get; set; }
        public ILookup<int, Attendance> AttendanceLookup { get; set; }
    }
}
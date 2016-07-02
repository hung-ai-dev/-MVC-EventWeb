using System.Collections.Generic;
using EventWeb.Models;

namespace EventWeb.ViewModels
{
    public class FollowingsViewModel
    {
        public FollowingsViewModel()
        {
        }

        public List<ApplicationUser> Followees { get; set; }
        public string Heading { get; set; }
        public bool ShowData { get; set; }
    }
}
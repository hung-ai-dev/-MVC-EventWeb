using EventWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventWeb.ViewModels
{
    public class GigFormViewModel
    {
        [Required]
        public string Venue { get; set; }
        
        [Required]
        public string Date { get; set; }

        [ValidTime]
        [Required]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
        public DateTime GetDateTime() {
            return DateTime.Parse($"{Date} {Time}");
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventWeb.Core.Models;

namespace EventWeb.Core.ViewModels
{
    public class GigFormViewModel
    {
        [Required]
        public int Id { get; set; }

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

        public string Heading { get; set; }

        public string Action
        {
            get { return (Id != 0) ? "Update" : "Create"; }
        }

        public DateTime GetDateTime() {
            return DateTime.Parse($"{Date} {Time}");
        }
    }
}
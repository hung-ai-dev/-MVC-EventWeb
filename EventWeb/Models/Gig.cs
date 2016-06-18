using System;
using System.ComponentModel.DataAnnotations;

namespace EventWeb.Models
{
    public class Gig
    {
        public byte Id { get; set; }

        [Required]
        public ApplicationUser Artist { get; set; }

        public DateTime DateTime { get; set; }

        [Required, MaxLength(255)]
        public string Venue { get; set; }

        [Required]
        public Genre Genre { get; set; }
    }
}
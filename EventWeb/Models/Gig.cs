using System;
using System.ComponentModel.DataAnnotations;

namespace EventWeb.Models
{
    public class Gig
    {
        public int Id { get; set; }

        [Required]
        public string ArtistId { get; set; }
<<<<<<< HEAD
=======
        public ApplicationUser Artist { get; set; }
>>>>>>> bea4ca28453729d004c49bea2965291f28d5a91a
        public DateTime DateTime { get; set; }

        [Required, MaxLength(255)]
        public string Venue { get; set; }

        [Required]
        public byte GenreId { get; set; }
<<<<<<< HEAD
=======
        public Genre Genre { get; set; }
>>>>>>> bea4ca28453729d004c49bea2965291f28d5a91a
    }
}
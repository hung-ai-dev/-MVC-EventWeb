using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EventWeb.Core.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public string ArtistId { get; set; }
        public ApplicationUser Artist { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public byte GenreId { get; set; }
        public Genre Genre { get; set; }

        public bool IsCanceled { get; private set; }
        public ICollection<Attendance> Attendances { get; set; }

        public void Cancel()
        {
            this.IsCanceled = true;

            var notification = new Notification(this, NotifycationType.GigCanceld);

            foreach (var attendee in this.Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        public void Modify(DateTime dateTime, string venue, byte genre)
        {
            var notification = new Notification(this, NotifycationType.GigUpdated);
            notification.OriginalDateTime = DateTime;
            notification.OriginalVenue = Venue;

            Venue = venue;
            DateTime = dateTime;
            GenreId = genre;

            foreach (var attendee in this.Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}
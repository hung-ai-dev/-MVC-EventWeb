using System;
using EventWeb.Core.Models;

namespace EventWeb.Core.Dtos
{ 
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotifycationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public GigDto Gig { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventWeb.Core.Models
{
    public class UserNotification
    {
        public string UserId { get; set; }

        public int NotificationId { get; set; }

        public ApplicationUser User { get; private set; }
        public Notification Notification { get; private set; }
        
        public bool IsRead { get; private set; }

        public UserNotification()
        {

        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            User = user;
            Notification = notification;
        }

        public void ReadNoti()
        {
            IsRead = true;
        }
    }
}
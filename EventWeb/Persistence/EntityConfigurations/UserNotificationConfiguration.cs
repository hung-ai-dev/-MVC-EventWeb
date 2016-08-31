using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using EventWeb.Core.Models;

namespace EventWeb.Persistence.EntityConfigurations
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            HasRequired(un => un.User)
                .WithMany(g => g.UserNotifications)
                .WillCascadeOnDelete(false);
            HasKey(un => new {un.UserId, un.NotificationId});
        }
    }
}
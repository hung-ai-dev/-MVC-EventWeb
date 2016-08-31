using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using EventWeb.Core.Models;

namespace EventWeb.Persistence.EntityConfigurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {

            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);

             HasMany(u => u.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);

            HasMany(u => u.Followees)
                    .WithRequired(f => f.Follower)
                    .WillCascadeOnDelete(false);
        }
    }
}
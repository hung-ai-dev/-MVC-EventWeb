using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventWeb.Models;

namespace EventWeb.Repositories
{
    public class FollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool GetFollowing(string artistId, string userId)
        {
            return _context.Followings.Any(a => a.FolloweeId == artistId && a.FollowerId == userId);
        }

    }
}
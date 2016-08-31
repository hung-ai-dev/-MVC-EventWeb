using System.Collections.Generic;
using System.Linq;
using EventWeb.Core.Models;
using EventWeb.Core.Repositories;

namespace EventWeb.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
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

        public List<ApplicationUser> GetMineFollowing(string userId)
        {
            return _context.Followings.Where(u => u.FollowerId == userId).Select(g => g.Followee).ToList();
        }
    }
}
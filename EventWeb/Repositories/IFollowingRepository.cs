using System.Collections.Generic;
using EventWeb.Models;

namespace EventWeb.Repositories
{
    public interface IFollowingRepository
    {
        bool GetFollowing(string artistId, string userId);
        List<ApplicationUser> GetMineFollowing(string userId);
    }
}
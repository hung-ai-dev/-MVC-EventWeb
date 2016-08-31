using System.Collections.Generic;
using EventWeb.Core.Models;

namespace EventWeb.Core.Repositories
{
    public interface IFollowingRepository
    {
        bool GetFollowing(string artistId, string userId);
        List<ApplicationUser> GetMineFollowing(string userId);
    }
}
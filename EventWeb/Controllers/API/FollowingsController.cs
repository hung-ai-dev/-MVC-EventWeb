using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventWeb.Core.Dtos;
using EventWeb.Core.Models;
using EventWeb.Persistence;
using Microsoft.AspNet.Identity;

namespace EventWeb.Controllers.API
{   
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            if (_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId))
                return BadRequest("Following already exists");

            var following = new Following()
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };
            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnFollow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var following = _context.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId);
            if (following == null)
                return NotFound();
            _context.Followings.Remove(following);
            _context.SaveChanges();
            return Ok();
        }
    }
}

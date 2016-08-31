using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using EventWeb.Core.Dtos;
using EventWeb.Core.Models;
using EventWeb.Persistence;
using Microsoft.AspNet.Identity;

namespace EventWeb.Controllers.API
{
    public class GigsController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(GigDto gigDto)
        {
            var userId = User.Identity.GetUserId();

            var gig = _unitOfWork.GigRepository.GetGigWithAttendee(gigDto.GigId);

            if (gig.IsCanceled)
                return NotFound();
            if (gig.ArtistId != userId)
                return Unauthorized();

            gig.Cancel();

            _unitOfWork.Complete();
            return Ok();
        }

    }
}

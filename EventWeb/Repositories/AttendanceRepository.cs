using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventWeb.Models;

namespace EventWeb.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Attendance> GetFutureAttendance(string userId)
        {
            return _context.Attendances.Where(a => a.AttendeeId == userId)
                .ToList();
        }

        public bool GetAttendance(int id, string userId)
        {
            return _context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == id);
        }
    }
}
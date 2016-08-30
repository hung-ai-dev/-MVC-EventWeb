using System.Collections.Generic;
using EventWeb.Models;

namespace EventWeb.Repositories
{
    public interface IAttendanceRepository
    {
        List<Attendance> GetFutureAttendance(string userId);
        bool GetAttendance(int id, string userId);
    }
}
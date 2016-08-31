using System.Collections.Generic;
using EventWeb.Core.Models;

namespace EventWeb.Core.Repositories
{
    public interface IAttendanceRepository
    {
        List<Attendance> GetFutureAttendance(string userId);
        bool GetAttendance(int id, string userId);
    }
}
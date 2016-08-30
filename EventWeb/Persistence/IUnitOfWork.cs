using EventWeb.Repositories;

namespace EventWeb.Persistence
{
    public interface IUnitOfWork
    {
        IGigRepository GigRepository { get; }
        IAttendanceRepository AttendanceRepository { get; }
        IFollowingRepository FollowingRepository { get; }
        IGenreRepository GenreRepository { get; }
        void Complete();
    }
}
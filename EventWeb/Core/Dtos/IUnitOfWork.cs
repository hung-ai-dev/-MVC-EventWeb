using EventWeb.Core.Repositories;

namespace EventWeb.Core.Dtos
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
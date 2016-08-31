using EventWeb.Core.Dtos;
using EventWeb.Core.Models;
using EventWeb.Core.Repositories;
using EventWeb.Persistence.Repositories;

namespace EventWeb.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGigRepository GigRepository { get; private set; }
        public IAttendanceRepository AttendanceRepository { get; private set; }
        public IFollowingRepository FollowingRepository { get; private set; }
        public IGenreRepository GenreRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            GigRepository = new GigRepository(context);
            AttendanceRepository = new AttendanceRepository(context);
            FollowingRepository = new FollowingRepository(context);
            GenreRepository = new GenreRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
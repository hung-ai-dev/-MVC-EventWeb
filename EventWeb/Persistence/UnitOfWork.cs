using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventWeb.Models;
using EventWeb.Repositories;

namespace EventWeb.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public GigRepository GigRepository { get; private set; }
        public AttendanceRepository AttendanceRepository { get; private set; }
        public FollowingRepository FollowingRepository { get; private set; }
        public GenreRepository GenreRepository { get; private set; }

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
using System;
using Won.Api.Repositories.Interfaces;

namespace Won.Api.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly WonDbContext _context;

        public TripRepository(WonDbContext context)
        {
            _context = context;
        }
    }
}

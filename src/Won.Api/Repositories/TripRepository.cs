using Won.Api.Repositories.Interfaces;
using Won.Api.Entities;
using Won.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Won.Api.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly WonDbContext _context;

        public TripRepository(WonDbContext context)
        {
            _context = context;
        }
        public async Task<List<Trip>> GetTripsAsync()
        {
            return await _context.Trips.ToListAsync();
        }
        public async Task<Trip?> GetTripByIdAsync(int id)
        {
            return await _context.Trips.FindAsync(id);
        }
    }
}

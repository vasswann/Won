using Microsoft.EntityFrameworkCore;

using Won.Api.Data;
using Won.Api.Entities;
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
        public async Task<List<Trip>> GetTripsAsync()
        {
            return await _context.Trips.ToListAsync();
        }
        public async Task<Trip?> GetTripByIdAsync(int id)
        {
            return await _context.Trips.FindAsync(id);
        }
        public async Task<Trip> CreateTripAsync(Trip trip)
        {
            _context.Trips.Add(trip);

            await _context.SaveChangesAsync();

            return trip;
        }
        public async Task<Trip?> UpdateTripAsync(Trip trip)
        {
            await _context.SaveChangesAsync();

            return trip;
        }
        public async Task<bool> DeleteTripAsync(int id)
        {
            var trip = await _context.Trips.FindAsync(id);

            if (trip == null)
            {
                return false;
            }

            _context.Trips.Remove(trip);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

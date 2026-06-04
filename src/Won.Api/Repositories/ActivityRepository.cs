using Microsoft.EntityFrameworkCore;
using Won.Api.Data;
using Won.Api.Entities;
using Won.Api.Repositories.Interfaces;

namespace Won.Api.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly WonDbContext _context;

        public ActivityRepository(WonDbContext context)
        {
            _context = context;
        }

        public async Task<List<Activity>> GetActivitiesByTripIdAsync(int tripId)
        {
            return await _context.Activities.Where(x => x.TripId == tripId)
                .ToListAsync();
        }

        public async Task<Activity?> GetActivityByIdAsync(int activityId)
        {
            return await _context.Activities.FindAsync(activityId);
        }

        public async Task<Activity> CreateActivityAsync(Activity activity)
        {
            _context.Activities.Add(activity);

            await _context.SaveChangesAsync();

            return activity;
        }

        public async Task<Activity?> UpdateActivityAsync(Activity activity)
        {
            await _context.SaveChangesAsync();

            return activity;
        }

        public async Task<bool> DeleteActivityAsync(int activityId)
        {
            var activity = await _context.Activities.FindAsync(activityId);

            if (activity == null)
            {
                return false;
            }

            _context.Activities.Remove(activity);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

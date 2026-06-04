using Won.Shared.Dtos;

namespace Won.Web.Services.Trips;

public class FakeTripService
{
    private readonly List<TripDto> _trips =
    [
        new()
        {
            TripId = 1,
            Name = "Budapest Weekend",
            Location = "Budapest",
            StartDate = DateTime.Today.AddDays(4),
            EndDate = DateTime.Today.AddDays(7),
            Details = "A short weekend trip to explore Budapest.",
            Budget = 350,
            GroupSize = 2
        },
        new()
        {
            TripId = 2,
            Name = "Salonta City Break",
            Location = "Salonta",
            StartDate = DateTime.Today.AddDays(13),
            EndDate = DateTime.Today.AddDays(16),
            Details = "Relaxed city break with food, architecture, and walking.",
            Budget = 220,
            GroupSize = 3
        },
        new()
        {
            TripId = 3,
            Name = "Summer Trip to Malta",
            Location = "Valetta",
            StartDate = DateTime.Today.AddMonths(2),
            EndDate = DateTime.Today.AddMonths(2).AddDays(5),
            Details = "Future trip. Weather forecast should not be available yet.",
            Budget = 500,
            GroupSize = 4
        }
    ];

    public Task<List<TripListItemDto>> GetTripsAsync()
    {
        var trips = _trips.Select(trip => new TripListItemDto
        {
            TripId = trip.TripId,
            Name = trip.Name,
            Location = trip.Location,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate
        }).ToList();

        return Task.FromResult(trips);
    }

    public Task<TripDto?> GetTripByIdAsync(int tripId)
    {
        var trip = _trips.FirstOrDefault(trip => trip.TripId == tripId);

        return Task.FromResult(trip);
    }
}
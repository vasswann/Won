using Won.Api.Entities;
using Won.Api.Repositories.Interfaces;
using Won.Api.Services.Interfaces;
using Won.Shared.Dtos;

namespace Won.Api.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }
        public async Task<List<Trip>> GetTripsAsync()
        {
            return await _tripRepository.GetTripsAsync();
        }
        public async Task<Trip?> GetTripByIdAsync(int id)
        {
            return await _tripRepository.GetTripByIdAsync(id);
        }
        public async Task<Trip> CreateTripAsync(CreateTripDto tripData)
        {
            var trip = new Trip
            {
                Name = tripData.Name,
                StartDate = tripData.StartDate,
                EndDate = tripData.EndDate,
                Location = tripData.Location,
                Details = tripData.Details,
                Budget = tripData.Budget,
                GroupSize = tripData.GroupSize
            };

            return await _tripRepository.CreateTripAsync(trip);
        }
        public async Task<Trip?> UpdateTripAsync(int id, UpdateTripDto updatedTripData)
        {
            var trip = await _tripRepository.GetTripByIdAsync(id);

            if (trip == null)
            {
                return null;
            }

            trip.Name = updatedTripData.Name;
            trip.StartDate = updatedTripData.StartDate;
            trip.EndDate = updatedTripData.EndDate;
            trip.Location = updatedTripData.Location;
            trip.Details = updatedTripData.Details;
            trip.Budget = updatedTripData.Budget;
            trip.GroupSize = updatedTripData.GroupSize;

            return await _tripRepository.UpdateTripAsync(trip);
        }
    }
}

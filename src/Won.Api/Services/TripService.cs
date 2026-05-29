using Won.Api.Entities;
using Won.Api.Repositories.Interfaces;
using Won.Api.Services.Interfaces;
using Won.Shared.Dtos;
using Won.Api.Exceptions;

namespace Won.Api.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }
        private static TripDto MapToDto(Trip trip)
        {
            return new TripDto
            {
                TripId = trip.TripId,
                Name = trip.Name,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Location = trip.Location
            };
        }

        public async Task<List<TripDto>> GetTripsAsync()
        {
            var trips = await _tripRepository.GetTripsAsync();
            return trips.Select(MapToDto).ToList();
        }
        public async Task<TripDto?> GetTripByIdAsync(int id)
        {
            var trip = await _tripRepository.GetTripByIdAsync(id);
            if(trip == null)
            {
                throw new NotFoundException($"Trip with ID {id} was not found.");
            }
            return MapToDto(trip);
        }
        public async Task<TripDto> CreateTripAsync(CreateTripDto tripData)
        {
            if (tripData.EndDate < tripData.StartDate)
            {
                throw new BadRequestException("End date cannot be before start date.");
            }
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

            var created = await _tripRepository.CreateTripAsync(trip);
            return MapToDto(created);
        }
        public async Task<TripDto?> UpdateTripAsync(int id, UpdateTripDto updatedTripData)
        {
            var trip = await _tripRepository.GetTripByIdAsync(id);

            if (trip == null)
            {
                throw new NotFoundException($"Trip with ID {id} was not found.");
            }

            trip.Name = updatedTripData.Name;
            trip.StartDate = updatedTripData.StartDate;
            trip.EndDate = updatedTripData.EndDate;
            trip.Location = updatedTripData.Location;
            trip.Details = updatedTripData.Details;
            trip.Budget = updatedTripData.Budget;
            trip.GroupSize = updatedTripData.GroupSize;

            var updated = await _tripRepository.UpdateTripAsync(trip);
            return updated == null ? null : MapToDto(updated);
        }
        public async Task<bool> DeleteTripAsync(int id)
        {
            var deleted = await _tripRepository.DeleteTripAsync(id);
            if (!deleted)
            {
                throw new NotFoundException($"Trip with ID {id} was not found.");

            }
            return deleted;
        }
    }
}

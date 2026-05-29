using Won.Api.Entities;
using Won.Shared.Dtos;

namespace Won.Api.Repositories.Interfaces
{
    public interface ITripRepository
    {
        Task<List<Trip>> GetTripsAsync();
        Task<Trip?> GetTripByIdAsync(int id);
        Task<Trip> CreateTripAsync(Trip trip);
        Task<Trip?> UpdateTripAsync(Trip trip);
        Task<bool> DeleteTripAsync(int id);
    }
}

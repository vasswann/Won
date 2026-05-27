using Won.Shared.Dtos;
namespace Won.Api.Services.Interfaces
{
    public interface ITripService
    {
        Task<List<Trip>> GetTripsAsync();
        Task<Trip?> GetTripByIdAsync(int id);
        Task<Trip> CreateTripAsync(CreateTripDto tripData);
        Task<Trip?> UpdateTripAsync(int id, UpdateTripDto updatedTripData);
        Task<bool> DeleteTripAsync(int id);
    }
}

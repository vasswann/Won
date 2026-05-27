using Won.Shared.Dtos;
namespace Won.Api.Services.Interfaces
{
    public interface ITripService
    {
        Task<List<Trip>> GetTripsAsync();
        Task<Trip?> GetTripByIdAsync(int id);
        Task<Trip> CreateTripAsync(CreateTripDto dto);
        Task<Trip?> UpdateTripAsync(int id, UpdateTripDto dto);
        Task<bool> DeleteTripAsync(int id);
    }
}

using Won.Api.Entities;

namespace Won.Api.Repositories.Interfaces
{
    public interface ITripRepository
    {
        Task<List<Trip>> GetTripsAsync();
        Task<Trip?> GetTripByIdAsync(int id);
    }
}

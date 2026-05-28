using Won.Shared.Dtos;
using Won.Api.Entities;

namespace Won.Api.Services.Interfaces
{
    public interface ITripService
    {
        Task<List<Trip>> GetTripsAsync();
    }
}

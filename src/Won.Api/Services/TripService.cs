using Won.Api.Entities;
using Won.Api.Repositories.Interfaces;
using Won.Api.Services.Interfaces;

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
    }
}

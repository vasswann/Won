using System;
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

    }
}

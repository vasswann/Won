using Microsoft.AspNetCore.Mvc;
using Won.Api.Services.Interfaces;
using Won.Shared.Dtos;

namespace Won.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }
    }
}

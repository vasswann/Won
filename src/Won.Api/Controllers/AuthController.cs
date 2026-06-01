using Microsoft.AspNetCore.Mvc;
using Won.Api.Services;
using Won.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Won.Api.Data;

namespace Won.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly WonDbContext _db;

        public AuthController(AuthService authService, WonDbContext db)
        {
            _authService = authService;
            _db = db;
        }
        
        //Test endpoint protected by JWT authentication
        [Authorize]
        [HttpGet("secret")]
        public IActionResult Secret()
        {
            return Ok("Middleware enforced Authenticated - Authenticated access granted");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            //retrieve user from database by email
            //Checks: for each user in DB userTable, check if u.Email = request.Email from FE

            var token =
                await _authService.LoginAsync(
                    request,
                    _db);

            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(
                new LoginResponseDto
                {
                    Token = token
                });
        }

        //Register endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterRequestDto request)
        {
            bool success =
                await _authService.RegisterAsync(
                    request,
                    _db);

            if(!success)
            {
                return BadRequest();
            }

            //Return success
            return Ok("User registered successfully!");
            
        }
    }
}

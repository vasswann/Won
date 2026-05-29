using Microsoft.AspNetCore.Mvc;
using Won.Api.Entities;
using Won.Api.Services;
using Won.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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

        //hardcoded test to see if middleware enforces authentication
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
            var user = await _db.Users
                .FirstOrDefaultAsync(
                u => u.Email == request.Email);

            if (user == null)
            {
                return Unauthorized();
            }

            bool valid =
                _authService.VerifyPassword(
                    user,
                    request.Password
                    );
            //checks if entered password matches the stored hash ^

            //if not valid, return unauthorized, if valid, return token:
            if (!valid)
            {
                return Unauthorized();
            }

            //return token:
            var token = _authService.GenerateToken(user);

            return Ok(
                new LoginResponseDto
                {
                    Token = token
                });
        }

    }
}

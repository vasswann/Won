using Microsoft.AspNetCore.Mvc;
using Won.Api.Entities;
using Won.Api.Services;
using Won.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Won.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        //hardcoded test to see if middleware enforces authentication
        [Authorize]
        [HttpGet("secret")]
        public IActionResult Secret()
        {
            return Ok("Middleware enforced Authenticated - Authenticated access granted");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequestDto request)
        {
            //hardcoding fake user for test
            var user = new User
            {
                Id = 1,
                Email = "test@test.com",

                PasswordHash =
                _authService.HashPassword(
                    new User(),
                    "password123"
                    )
            };

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

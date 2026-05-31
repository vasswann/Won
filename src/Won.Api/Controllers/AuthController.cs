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

        //Register endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterRequestDto request)
        {
            //Check Email - existingUser looks through all dbUsers and finds the first one with an email which matches the requestEmail
            var existingUser =
                await _db.Users
                .FirstOrDefaultAsync(
                    u => u.Email == request.Email);

            //if the user already exists(is registered), throw BadRequest
            if(existingUser != null)
            {
                return BadRequest(
                    "Email already exists");
            }

            else if(string.IsNullOrWhiteSpace(request.Email) || 
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Email and password are required");
            }

            //Create new user
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow
            };

            //Hash their password with method in authService
            user.PasswordHash =
                _authService.HashPassword(
                    user,
                    request.Password);

            //Save the new user to the db
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            //Return success
            return Ok("User registered successfully!");
            
        }
    }
}

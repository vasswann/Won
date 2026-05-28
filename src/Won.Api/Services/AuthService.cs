using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Won.Api.Entities;

namespace Won.Api.Services
{
    public class AuthService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        
        public AuthService(IPasswordHasher<User> passwordHasher)
        {
            
            _passwordHasher = passwordHasher;
        }

        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(
                user,
                password);
        }
        //this creates a HashPassword

        public bool VerifyPassword(User user, string enteredPassword)
        {
            var result =
                _passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    enteredPassword
                    );
            // ^ this compares enteredPassword against user.PasswordHash in database (without decrypting password - it does it by maths)
            // ^ user is also used as an argument just to give the hasher context
            return result == PasswordVerificationResult.Success;
        }

        public string GenerateToken(User user)
        {
            //this method receives authenticated user, creates JWT, returns token string
            //claims are identity information given in JSON, which will be embedded into the JWT
            var wonUserClaims = new[]
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    user.Id.ToString()
                    ),

                new Claim(
                    JwtRegisteredClaimNames.Email,
                    user.Email)


            };

            var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        Environment.GetEnvironmentVariable("JWT_KEY")!
                        )
                    );
            //This creates cryptographic signing key from secret (in .env)
            // ! means it won't be null

            var wonUserCreds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);
            // Uses claims, signing credentials and environment variables to generate JWT

            var wonUserToken = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
                audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                claims: wonUserClaims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: wonUserCreds
                );
            //This uses the created claims and credentials, and the info in Won.Api/appsettings.json to create token

            return new JwtSecurityTokenHandler().WriteToken(wonUserToken);
            //this uses a built-in to return the generated token as a string
            //this uses a built-in to return the generated token as a string
        }
    }
}

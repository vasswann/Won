using DotNetEnv;
//using Won.Api.Data;
using Microsoft.AspNetCore.Identity;
//using Won.Api.Repositories;
//using Won.Api.Repositories.Interfaces;
//using Won.Api.Services;
//using Won.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Won.Api.Entities;
using Won.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

Env.TraversePath().Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(); commented out because the default version does not know how to understand Bearer authentication
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Enter JWT token"
        });

    options.AddSecurityRequirement(
        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
});


//adding AuthService
builder.Services.AddScoped<AuthService>();
//builder.Services.AddScoped<ITripRepository, TripRepository>();
//builder.Services.AddScoped<ITripService, TripService>();

//Dependency Injection for AuthService to create a passwordhash
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


// JWT Middleware. Note: "bearer" means "whoever bears this troken is authenticated"

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var SecretjwtKey =
        Environment.GetEnvironmentVariable("JWT_KEY");

    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(SecretjwtKey!))
        };
});


// Database configuration
var connectionName = Environment.GetEnvironmentVariable("DB_CONNECTION_NAME")
    ?? "DefaultConnection";

var connectionString = builder.Configuration.GetConnectionString(connectionName);

var sqlPassword = Environment.GetEnvironmentVariable("SQL_PASSWORD");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException($"Connection string '{connectionName}' was not found.");
}

if (string.IsNullOrWhiteSpace(sqlPassword))
{
    throw new InvalidOperationException("SQL_PASSWORD environment variable was not found.");
}

connectionString = connectionString.Replace("{SQL_PASSWORD}", sqlPassword);

//builder.Services.AddDbContext<WonDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //var dbContext = scope.ServiceProvider.GetRequiredService<WonDbContext>();
    //dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Authentication for JWT middleware 
app.UseAuthentication();
app.UseAuthorization();

// .env smoke test
app.MapGet("/env-test", () =>
{
    var secret = Environment.GetEnvironmentVariable("SECRET_SMOKE_TEST");

    return Results.Ok(new
    {
        CurrentDirectory = Directory.GetCurrentDirectory(),
        BaseDirectory = AppContext.BaseDirectory,
        Secret = secret
    });
});

app.MapControllers();

app.Run();

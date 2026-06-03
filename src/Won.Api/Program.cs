using DotNetEnv;
using Won.Api.Repositories;
using Won.Api.Repositories.Interfaces;
using Won.Api.Services;
using Won.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Won.Api.Data;
using Won.Api.Middleware;

Env.TraversePath().Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<ITripService, TripService>();

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

builder.Services.AddDbContext<WonDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WonDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

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

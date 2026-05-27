using DotNetEnv;
using Won.Api.Repositories;
using Won.Api.Repositories.Interfaces;
using Won.Api.Services;
using Won.Api.Services.Interfaces;

Env.TraversePath().Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<ITripService, TripService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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

using Won.Web.Components;
using Won.Web.Services.Activities;
using Won.Web.Services.Locations;
using Won.Web.Services.Trips;
using Won.Web.Services.Weather;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("http://won.api:8080")
    });

builder.Services.AddScoped<FakeTripService>();
builder.Services.AddScoped<TripsService>();

builder.Services.AddScoped<WeatherService>();
builder.Services.AddScoped<LocationImageService>();
builder.Services.AddScoped<ActivitySuggestionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Won.Web.Client._Imports).Assembly);

app.Run();
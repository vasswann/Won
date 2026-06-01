using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

await builder.Build().RunAsync();

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7002")
    });

await builder.Build().RunAsync();
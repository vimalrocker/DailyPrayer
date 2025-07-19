using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Client.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// TEMP HttpClient for loading configuration
using var tempHttp = new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
};

// Load base appsettings.json
using var response = await tempHttp.GetAsync("appsettings.json");
if (response.IsSuccessStatusCode)
{
    using var stream = await response.Content.ReadAsStreamAsync();
    builder.Configuration.AddJsonStream(stream);
}

// Load environment-specific appsettings
var environment = builder.HostEnvironment.Environment;
using var envResponse = await tempHttp.GetAsync($"appsettings.{environment}.json");
if (envResponse.IsSuccessStatusCode)
{
    using var envStream = await envResponse.Content.ReadAsStreamAsync();
    builder.Configuration.AddJsonStream(envStream);
}

// Register HttpClient using config value or fallback
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? builder.HostEnvironment.BaseAddress;

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl)
});

// Register your application services
builder.Services.AddScoped<UserApiService>();

await builder.Build().RunAsync();
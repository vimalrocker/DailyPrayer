using Api;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker;
using Supabase;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(builder =>
    {
        builder.UseMiddleware<CorsMiddleware>();
    })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        
        // Add CORS services
        services.AddCors(options =>
        {
            options.AddPolicy("AllowBlazorClient", policy =>
            {
                policy.WithOrigins("http://dailyprayerapi-b3e5d8ebb4f0ekau.eastasia-01.azurewebsites.net", "https://dailyprayerapi-b3e5d8ebb4f0ekau.eastasia-01.azurewebsites.net")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        
        // Configure Supabase client
        services.AddSingleton<Client>(provider =>
        {
            var url = Environment.GetEnvironmentVariable("SupabaseUrl");
            var key = Environment.GetEnvironmentVariable("SupabaseKey");
     
            return new Client(url, key);
        });
    })
    .Build();

host.Run();
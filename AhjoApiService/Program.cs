using AhjoApiService.AhjoApi;
using AhjoApiService.AhjoApi.Models;
using AhjoApiService.StorageClient;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AhjoApiServiceUnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace AhjoApiService
{
    /// <summary>
    /// Application entry point for the datapumppu-paatokset-srv background polling service.
    /// Configures dependency injection and health checks, then runs an infinite polling loop
    /// that fetches, transforms, and persists meeting data from the Ahjo API.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Configures the ASP.NET Core host, registers all services, maps health check endpoints,
        /// and starts the background polling loop.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddHealthChecks();

            AddDependencyInjections(builder.Services);

            builder.Services.AddHostedService<PollingService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();

            app.MapHealthChecks("/healthz");
            app.MapHealthChecks("/readiness");

            app.MapControllers();
            
            app.Run();
        }

        /// <summary>
        /// Registers all application services into the dependency injection container.
        /// All services are registered with <see cref="ServiceLifetime.Transient"/> lifetime.
        /// </summary>
        /// <param name="servicess">The service collection to configure.</param>
        private static void AddDependencyInjections(IServiceCollection servicess)
        {
            servicess.AddTransient<IAhjoApiClient, AhjoApiClient>();
            servicess.AddTransient<IAhjoApiReader, AhjoApiReader>();
            servicess.AddTransient<IStorage, Storage>();
            servicess.AddTransient<IStorageApiClient, StorageApiClient>();
            servicess.AddTransient<IStorageConnection, StorageConnection>();
            servicess.AddTransient<IMeetingComparer, MeetingComparer>();
            servicess.AddTransient<IAhjoApiConnection, AhjoApiConnection>();            
        }

    }
}
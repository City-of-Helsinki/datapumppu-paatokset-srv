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

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/healthz");
                endpoints.MapHealthChecks("/readiness");
            });

            app.MapControllers();
            
            // Poll meeting data
            
            var apiReader = app.Services.GetService<IAhjoApiReader>();
            var storage = app.Services.GetService<IStorage>();

            Run(apiReader, storage);

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

        /// <summary>
        /// Runs the infinite background polling loop. Every 60 minutes, fetches meetings
        /// for a 7-day window, maps them to storage DTOs, and posts them to the Storage API.
        /// The start date advances by 7 days each iteration and resets to tomorrow when
        /// it exceeds 3 months into the future.
        /// </summary>
        /// <param name="apiReader">The Ahjo API reader for fetching meeting data.</param>
        /// <param name="storage">The storage service for persisting transformed meetings.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="apiReader"/> or <paramref name="storage"/> is <c>null</c>.</exception>
        private static async Task Run(IAhjoApiReader? apiReader, IStorage? storage)
        {
            if (apiReader == null)
            {
                throw new ArgumentNullException("apiReader");
            }

            if (storage == null)
            {
                throw new ArgumentNullException("storage");
            }

            const int PollingTime = 1000 * 60 * 60;
            const int DaysInOneTry = 7;
            var startDate = DateTime.UtcNow.AddDays(1);
            while (true)
            {
                var meetings = await apiReader.GetMeetingsData(startDate, startDate.AddDays(DaysInOneTry));
                var storageDtos = AhjoToStorageMapper.CreateStorageMeetingDTOs(meetings);
                await storage.Add(storageDtos);
                await Task.Delay(PollingTime);

                startDate = startDate.AddDays(DaysInOneTry);
                if (startDate > DateTime.UtcNow.AddMonths(3))
                {
                    startDate = DateTime.UtcNow.AddDays(1);
                }
            }
        }
    }
}
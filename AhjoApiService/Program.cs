using AhjoApiService.AhjoApi;
using AhjoApiService.AhjoApi.Models;
using AhjoApiService.StorageClient;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AhjoApiServiceUnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace AhjoApiService
{
    public class Program
    {
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
using AhjoApiService.AhjoApi;
using AhjoApiService.StorageClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AhjoApiServiceUnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace AhjoApiService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IAhjoApiClient, AhjoApiClient>();
                    services.AddTransient<IAhjoApiReader, AhjoApiReader>();

                    services.AddTransient<IStorageCache, StorageCache>();
                    services.AddTransient<IStorage, Storage>();
                    services.AddTransient<IStorageApiClient, StorageApiClient>();
                    services.AddTransient<IStorageConnection, StorageConnection>();
                    services.AddTransient<IMeetingComparer, MeetingComparer>();

                })
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    configuration.AddJsonFile($"appsettings.{environment}.json", true, true);
                })
                .Build();

            var apiReader = host.Services.GetService<IAhjoApiReader>();
            var storage = host.Services.GetService<IStorage>();
            Run(apiReader, storage).Wait();
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

            const int PollingTime = 60 * 1000;
            while (true)
            {
                var meetings = await apiReader.GetMeetingsData();
                var storageDtos = AhjoToStorageMapper.CreateStorageMeetingDTOs(meetings);
                await storage.Add(storageDtos);
                Thread.Sleep(PollingTime);
            }
        }
    }

}
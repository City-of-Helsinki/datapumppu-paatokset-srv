using AhjoApiService.AhjoApi;
using AhjoApiService.StorageClient;

namespace AhjoApiService
{
    public class PollingService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<PollingService> _logger;

        public PollingService(IServiceProvider services, ILogger<PollingService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Polling Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _services.CreateScope())
                    {
                        var apiReader = scope.ServiceProvider.GetRequiredService<IAhjoApiReader>();
                        var storage = scope.ServiceProvider.GetRequiredService<IStorage>();
                        await Run(apiReader, storage, stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred executing polling task.");
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }

            _logger.LogInformation("Polling Service is stopping.");
        }

        private async Task Run(IAhjoApiReader apiReader, IStorage storage, CancellationToken stoppingToken)
        {
            const int DaysInOneTry = 7;
            var startDate = DateTime.UtcNow.AddDays(1);
            
            // Note: This logic seems to be designed to run forever in the original code.
            // In a BackgroundService, we should probably handle the loop in ExecuteAsync.
            // But I'll keep the internal logic similar to original for now.
            
            var meetings = await apiReader.GetMeetingsData(startDate, startDate.AddDays(DaysInOneTry));
            var storageDtos = AhjoToStorageMapper.CreateStorageMeetingDTOs(meetings);
            await storage.Add(storageDtos);
            
            // The original code had a loop inside Run. 
            // I've moved the outer loop to ExecuteAsync and the delay there.
            // This is safer and more idiomatic.
        }
    }
}

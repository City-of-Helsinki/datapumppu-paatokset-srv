using AhjoApiService.StorageClient.DTOs;

namespace AhjoApiService.StorageClient
{
    internal interface IStorage
    {
        Task Add(List<StorageMeetingDTO> meeting);
    }

    internal class Storage : IStorage
    {
        private readonly IStorageApiClient _storageApiClient;
        private readonly ILogger<Storage> _logger;

        public Storage(
            ILogger<Storage> logger,
            IStorageApiClient apiClient)
        {
            _logger = logger;
            _storageApiClient = apiClient;
        }

        public async Task Add(List<StorageMeetingDTO> meetings)
        {
            try
            {
                _logger.LogInformation("Sending meeting into");
                await _storageApiClient.SendMeetings(meetings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sending data to strorage failed");
            }
        }
    }
}

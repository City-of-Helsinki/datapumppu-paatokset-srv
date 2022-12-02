using AhjoApiService.StorageClient.DTOs;

namespace AhjoApiService.StorageClient
{
    internal interface IStorage
    {
        Task Add(List<StorageMeetingDTO> meeting);
    }

    internal class Storage : IStorage
    {
        private readonly IStorageCache _storageCache;
        private readonly IStorageApiClient _storageApiClient;

        public Storage(IStorageCache storageCache,
            IStorageApiClient apiClient)
        {
            _storageCache = storageCache;
            _storageApiClient = apiClient;
        }

        public async Task Add(List<StorageMeetingDTO> meetings)
        {
            var updated = meetings.Where(meeting => _storageCache.IsUpdated(meeting))
                .ToList();

            if (await _storageApiClient.SendMeetings(updated))
            {
                _storageCache.Add(updated);
            }
        }
    }
}

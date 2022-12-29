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

        public Storage(IStorageApiClient apiClient)
        {
            _storageApiClient = apiClient;
        }

        public Task Add(List<StorageMeetingDTO> meetings)
        {
            return _storageApiClient.SendMeetings(meetings);
        }
    }
}

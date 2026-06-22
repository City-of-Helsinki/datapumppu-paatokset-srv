using AhjoApiService.StorageClient.DTOs;

namespace AhjoApiService.StorageClient
{
    /// <summary>
    /// Defines the high-level persistence contract for storing meeting data.
    /// </summary>
    internal interface IStorage
    {
        /// <summary>
        /// Persists the provided meetings to the Datapumppu Storage API.
        /// </summary>
        /// <param name="meeting">The meetings to store.</param>
        Task Add(List<StorageMeetingDTO> meeting);
    }

    /// <summary>
    /// Delegates meeting persistence to <see cref="IStorageApiClient"/> and catches any exceptions
    /// to ensure the calling polling loop is never interrupted by storage failures.
    /// </summary>
    internal class Storage : IStorage
    {
        private readonly IStorageApiClient _storageApiClient;
        private readonly ILogger<Storage> _logger;

        /// <summary>
        /// Initialises a new instance of <see cref="Storage"/>.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="apiClient">The underlying API client used to send meetings.</param>
        public Storage(
            ILogger<Storage> logger,
            IStorageApiClient apiClient)
        {
            _logger = logger;
            _storageApiClient = apiClient;
        }

        /// <inheritdoc />
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

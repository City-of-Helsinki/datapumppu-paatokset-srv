using AhjoApiService.StorageClient.DTOs;
using Newtonsoft.Json;
using System.Text;

namespace AhjoApiService.StorageClient
{
    /// <summary>
    /// Defines the low-level HTTP contract for posting meeting data to the Datapumppu Storage API.
    /// </summary>
    internal interface IStorageApiClient
    {
        /// <summary>
        /// Serialises and POSTs each meeting to the Storage API endpoint <c>/api/meetinginfo/meeting</c>.
        /// </summary>
        /// <param name="meeting">The meetings to send.</param>
        /// <returns><c>true</c> if all meetings were sent successfully; <c>false</c> if any request fails.</returns>
        Task<bool> SendMeetings(List<StorageMeetingDTO> meeting);
    }

    /// <summary>
    /// HTTP client that serialises <see cref="StorageMeetingDTO"/> objects as JSON and POSTs them
    /// to the Datapumppu Storage API at <c>/api/meetinginfo/meeting</c>. Returns <c>false</c> on
    /// the first failed request (fail-fast).
    /// </summary>
    internal class StorageApiClient : IStorageApiClient
    {
        private readonly IStorageConnection _storageConnection;
        private readonly ILogger<StorageApiClient> _logger;

        /// <summary>
        /// Initialises a new instance of <see cref="StorageApiClient"/>.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="storageConnection">Factory for creating HTTP connections to the Storage API.</param>
        public StorageApiClient(ILogger<StorageApiClient> logger,
            IStorageConnection storageConnection)
        {
            _logger = logger;
            _storageConnection = storageConnection;
        }

        /// <inheritdoc />
        public async Task<bool> SendMeetings(List<StorageMeetingDTO> meetings)
        {
            _logger.LogInformation("SendMeetings()");
            foreach (var meeting in meetings)
            {
                _logger.LogInformation($"Sending meeting: {meeting.MeetingID}");

                const string ContentType = "application/json";
                using (var connection = _storageConnection.CreateConnection())
                {
                    var message = new HttpRequestMessage(HttpMethod.Post, "api/meetinginfo/meeting");
                    var jsonContent = JsonConvert.SerializeObject(meeting);
                    message.Content = new StringContent(jsonContent, Encoding.UTF8, ContentType);

                    var result = await connection.SendAsync(message);
                    if (!result.IsSuccessStatusCode)
                    {
                        _logger.LogError($"SendMeetings, error ${result.StatusCode}");
                        return false;
                    }
                }
            }

            return true;
        }
    }
}

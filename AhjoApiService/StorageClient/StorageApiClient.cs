using AhjoApiService.StorageClient.DTOs;
using Newtonsoft.Json;
using System.Text;

namespace AhjoApiService.StorageClient
{
    internal interface IStorageApiClient
    {
        Task<bool> SendMeetings(List<StorageMeetingDTO> meeting);
    }

    internal class StorageApiClient : IStorageApiClient
    {
        private readonly IStorageConnection _storageConnection;
        private readonly ILogger<StorageApiClient> _logger;

        public StorageApiClient(ILogger<StorageApiClient> logger,
            IStorageConnection storageConnection)
        {
            _logger = logger;
            _storageConnection = storageConnection;
        }

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

using Xunit;
using Moq;
using AhjoApiService.StorageClient;
using Microsoft.Extensions.Logging;
using AhjoApiService.StorageClient.DTOs;

namespace AhjoApiServiceUnitTests.StorageClient
{
    public class StorageTetss
    {
        [Fact]
        public async void SendShouldUseApiClient()
        {
            var logger = new Mock<ILogger<Storage>>();
            var apiClient = new Mock<IStorageApiClient>();

            var storage = new Storage(logger.Object, apiClient.Object);
            await storage.Add(new List<StorageMeetingDTO>());
            apiClient.Verify(x => x.SendMeetings(It.IsAny<List<StorageMeetingDTO>>()), Times.Once);
        }

        [Fact]
        public async void SendShouldNotThrowIfApiClientThrows()
        {
            var logger = new Mock<ILogger<Storage>>();
            var apiClient = new Mock<IStorageApiClient>();
            apiClient.Setup(x => x.SendMeetings(It.IsAny<List<StorageMeetingDTO>>())).Throws(new Exception());

            var storage = new Storage(logger.Object, apiClient.Object);
            await storage.Add(new List<StorageMeetingDTO>());
            apiClient.Verify(x => x.SendMeetings(It.IsAny<List<StorageMeetingDTO>>()), Times.Once);
        }

    }
}

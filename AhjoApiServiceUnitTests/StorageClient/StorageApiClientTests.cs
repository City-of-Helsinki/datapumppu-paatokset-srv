using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using AhjoApiService.StorageClient;
using AhjoApiService.StorageClient.DTOs;
using Moq.Protected;
using System.Net;

namespace AhjoApiServiceUnitTests.StorageClient
{
    public class StorageApiClientTests
    {
        [Fact]
        public async void SendMeetingsShouldReturnFalseIfSendFails()
        {
            var logger = new Mock<ILogger<StorageApiClient>>();
            var storageConnection = new Mock<IStorageConnection>();
            var storageApiClient = new StorageApiClient(logger.Object, storageConnection.Object);

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Loose);
            handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
           .ReturnsAsync(new HttpResponseMessage()
           {
               StatusCode = HttpStatusCode.BadRequest,
           })
           .Verifiable();

            storageConnection.Setup(x => x.CreateConnection()).Returns(() => {
                return new HttpClient(handlerMock.Object)
                {
                    BaseAddress = new Uri("http://test.com/"), // use your real URL here
                };
            });

            var meetings = new List<StorageMeetingDTO>
            {
                new StorageMeetingDTO { MeetingID = "1" }
            };

            var result = await storageApiClient.SendMeetings(meetings);

            Assert.False(result);
        }

        [Fact]
        public async void SendMeetigsShouldSendAllMessages()
        {
            var logger = new Mock<ILogger<StorageApiClient>>();
            var storageConnection = new Mock<IStorageConnection>();
            var storageApiClient = new StorageApiClient(logger.Object, storageConnection.Object);


            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Loose);
            handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
           .ReturnsAsync(new HttpResponseMessage()
           {
               StatusCode = HttpStatusCode.OK,
           })
           .Verifiable();

            storageConnection.Setup(x => x.CreateConnection()).Returns(() => {
                return new HttpClient(handlerMock.Object)
                {
                    BaseAddress = new Uri("http://test.com/"), // use your real URL here
                };
            });

            var meetings = new List<StorageMeetingDTO>
            {
                new StorageMeetingDTO { MeetingID = "1" },
                new StorageMeetingDTO { MeetingID = "1" },
                new StorageMeetingDTO { MeetingID = "1" }
            };

            await storageApiClient.SendMeetings(meetings);

            storageConnection.Verify(x => x.CreateConnection(), Times.Exactly(3));
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(3),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Post
                    && req.RequestUri == new Uri("http://test.com/api/meetinginfo/meeting")
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}
using AhjoApiService.AhjoApi;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;
using Xunit;

public class AhjoApiClientTests
{
    [Fact]
    public async Task GetMeetings_ReturnsMeetings_WhenResponseIsSuccess()
    {
            
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
               Content = new StringContent("{\"Meetings\":[{\"DateMeeting\":\"2021-05-12T00:00:00\",\"MeetingID\":\"2021-05-12T00:00:00\",\"DecisionMaker\":\"02900\",\"DecisionMakerID\":\"02900\",\"Name\":\"Kokous\",\"Location\":\"\",\"AgendaPublished\":false,\"MinutesPublished\":false},{\"DateMeeting\":\"2021-05-13T00:00:00\",\"MeetingID\":\"2021-05-13T00:00:00\",\"DecisionMaker\":\"02900\",\"DecisionMakerID\":\"02900\",\"Name\":\"Kokous\",\"Location\":\"\",\"AgendaPublished\":false,\"MinutesPublished\":false}]}"),
           })
           .Verifiable();

        var loggerMock = new Mock<ILogger<AhjoApiClient>>();
        var connectionMock = new Mock<IAhjoApiConnection>();
        connectionMock.Setup(x => x.CreateConnection()).Returns(() => 
        {
            return new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"), // use your real URL here
            };
        });
        var client = new AhjoApiClient(loggerMock.Object, connectionMock.Object);

        // Act
        var result = await client.GetMeetings(DateTime.Now, DateTime.Now.AddDays(1));

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Length);
        handlerMock.Protected().Verify(
           "SendAsync",
           Times.Exactly(1), // we expected a single external request
           ItExpr.IsAny<HttpRequestMessage>(),
           ItExpr.IsAny<CancellationToken>());
    }
}
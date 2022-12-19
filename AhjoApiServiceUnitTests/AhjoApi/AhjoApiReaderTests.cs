using AhjoApiService.AhjoApi;
using AhjoApiService.AhjoApi.DTOs;
using AhjoApiService.AhjoApi.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AhjoApiServiceUnitTests.AhjoApi
{
    public class AhjoApiReaderTests
    {
        [Fact]
        public async Task GetMeetingsData_AhjoMeetingData_IsCorrect()
        {
            var meetings = new List<AhjoMeetingDTO>
            {
                new AhjoMeetingDTO
                {
                    MeetingID = "2",
                }
            };
            var fullMeeting = new AhjoFullMeetingDTO {};

            var fullDecisions = new AhjoFullDecisionDTO[]
            {
                new AhjoFullDecisionDTO {}
            };

            var ahjoMeetingData = new AhjoMeetingData(fullMeeting);
            var mockLogger = new Mock<ILogger<AhjoApiReader>>();
            var mockAhjoApiClient = new Mock<IAhjoApiClient>();
            mockAhjoApiClient.Setup(m => m.GetMeetings()).ReturnsAsync(meetings.ToArray());
            mockAhjoApiClient.Setup(m => m.GetMeetingDetails(meetings[0])).ReturnsAsync(fullMeeting);
            mockAhjoApiClient.Setup(m => m.GetDecisions(meetings[0].MeetingID)).ReturnsAsync(fullDecisions);

            var apiReader = new AhjoApiReader(mockAhjoApiClient.Object, mockLogger.Object);
            var result = await apiReader.GetMeetingsData();

            Assert.IsType<AhjoMeetingData>(result[0]);
            Assert.IsType<AhjoFullMeetingDTO>(result[0].FullMeeting);
            Assert.IsType<AhjoFullDecisionDTO[]>(result[0].Decisions);
        }
    }
}

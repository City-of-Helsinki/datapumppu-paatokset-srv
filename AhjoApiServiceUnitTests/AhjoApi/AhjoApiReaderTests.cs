using AhjoApiService.AhjoApi;
using AhjoApiService.AhjoApi.DTOs;
using AhjoApiService.AhjoApi.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace AhjoApiServiceUnitTests.AhjoApi
{
    public class AhjoApiReaderTests
    {
        [Fact]
        public async Task GetMeetings_AhjoMeetingData_IsCorrectWithoutAgendasAsync()
        {
            var meetings = new List<AhjoMeetingDTO>
            {
                new AhjoMeetingDTO
                {
                    MeetingID = "2",
                    Name = "Valtuuston kokous",
                    DateMeeting = new DateTime(2023, 1, 1, 17, 0, 0),
                    AgendaPublished = false,
                }
            };
            var ahjoMeetingData = new AhjoMeetingData(meetings[0]);
            var mockLogger = new Mock<ILogger<AhjoApiReader>>();
            var mockAhjoApiClient = new Mock<IAhjoApiClient>();
            mockAhjoApiClient.Setup(m => m.GetMeetings()).ReturnsAsync(meetings.ToArray());

            var apiReader = new AhjoApiReader(mockAhjoApiClient.Object, mockLogger.Object);
            var result = await apiReader.GetMeetingsData();

            Assert.Equal(JsonConvert.SerializeObject(ahjoMeetingData), JsonConvert.SerializeObject(result[0]));
        }

        [Fact]
        public async Task GetMeetings_AhjoMeetingData_IsCorrectWithAgendasAsync()
        {
            var meetingDate = new DateTime(2023, 1, 1, 17, 0, 0);
            var meetings = new List<AhjoMeetingDTO>
            {
                new AhjoMeetingDTO
                {
                    MeetingID = "2",
                    Name = "Valtuuston kokous",
                    DateMeeting = meetingDate,
                    AgendaPublished = true,
                }
            };
            var fullMeeting = new AhjoFullMeetingDTO
            {
                MeetingID = "1",
                Name = "Valtuuston kokous",
                DateMeeting = meetingDate,
                MeetingSequenceNumber = 1,
                Agenda = new AhjoAgendaItemDTO[]
                    {
                        new AhjoAgendaItemDTO
                        {
                            AgendaPoint = 44,
                            Section = "sfas12312d",
                            AgendaItem = "otsikko1",
                            CaseIDLabel = "s42124121",
                            Html = "sd123212",
                            DecisionHistoryHTML = "s2341232d",
                        },
                        new AhjoAgendaItemDTO
                        {
                            AgendaPoint = 66,
                            Section = "shgfasdf",
                            AgendaItem = "otsikko2",
                            CaseIDLabel = "s3asdf12",
                            Html = "sddaasdff3",
                            DecisionHistoryHTML = "sdfasdfsdd",
                        }
                    }
            };
            var ahjoMeetingData = new AhjoMeetingData(meetings[0], fullMeeting.Agenda);
            var mockLogger = new Mock<ILogger<AhjoApiReader>>();
            var mockAhjoApiClient = new Mock<IAhjoApiClient>();
            mockAhjoApiClient.Setup(m => m.GetMeetings()).ReturnsAsync(meetings.ToArray());
            mockAhjoApiClient.Setup(m => m.GetMeetingDetails(meetings[0])).ReturnsAsync(fullMeeting);

            var apiReader = new AhjoApiReader(mockAhjoApiClient.Object, mockLogger.Object);
            var result = await apiReader.GetMeetingsData();

            Assert.Equal(JsonConvert.SerializeObject(ahjoMeetingData), JsonConvert.SerializeObject(result[0]));
        }
    }
}

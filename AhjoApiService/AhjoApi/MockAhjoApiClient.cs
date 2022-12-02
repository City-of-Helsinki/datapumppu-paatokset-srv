using AhjoApiService.AhjoApi.DTOs;
using Microsoft.Extensions.Logging;

namespace AhjoApiService.AhjoApi
{
    internal class MockAhjoApiClient : IAhjoApiClient
    {
        private readonly ILogger<MockAhjoApiClient> _logger;

        public MockAhjoApiClient(ILogger<MockAhjoApiClient> logger)
        {
            this._logger = logger;
        }
        public async Task<AhjoMeetingDTO[]?> GetMeetings()
        {
            _logger.LogInformation("Executing GetMeetings()");

            var meetingDate = new DateTime(2023, 1, 1, 17, 0, 0);
            var result = new List<AhjoMeetingDTO>
            {
                new AhjoMeetingDTO
                {
                    MeetingID = "1",
                    Name = "Valtuuston kokous",
                    DateMeeting = meetingDate,
                    AgendaPublished = true,
                },
                new AhjoMeetingDTO
                {
                    MeetingID = "2",
                    Name = "Valtuuston kokous",
                    DateMeeting = meetingDate.AddMonths(1),
                    AgendaPublished = false,
                }
            };
            return await Task.FromResult(result.ToArray());
        }

        public async Task<AhjoFullMeetingDTO?> GetMeetingDetails(AhjoMeetingDTO meetingDTO)
        {
            _logger.LogInformation("Executing GetMeetingDetails()");

            if (meetingDTO.AgendaPublished == true || meetingDTO.MinutesPublished == true)
            {
                var meetingDate = new DateTime(2023, 1, 1, 17, 0, 0);
                var result = new AhjoFullMeetingDTO
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
                return await Task.FromResult(result);
            }
            return await Task.FromResult(new AhjoFullMeetingDTO());
        }

        public async Task<AhjoFullDecisionDTO[]> GetDecisions(string meetingID)
        {

            var result = new List<AhjoFullDecisionDTO>
            {
                new ()
                {
                    NativeId = "1",
                    Title = "Päätös 1234",
                    CaseIDLabel = "caseidlabel1",
                    CaseID = "caseid1",
                    Section = "1",
                    Content = "html1",
                    Motion = "motion1",
                    ClassificationCode = "ccode1",
                    ClassificationTitle = "ctitle1"

                },
                new ()
                {
                    NativeId = "2",
                    Title = "Päätös 33334",
                    CaseIDLabel = "caseidlabel2",
                    CaseID = "caseid2",
                    Section = "2",
                    Content = "html2",
                    Motion = "motion2",
                    ClassificationCode = "ccode2",
                    ClassificationTitle = "ctitle2"
                },
            };

            return await Task.FromResult(result.ToArray());
        }
    }
}
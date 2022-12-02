using AhjoApiService.AhjoApi.DTOs;
using AhjoApiService.AhjoApi.Models;
using Microsoft.Extensions.Logging;

namespace AhjoApiService.AhjoApi
{
    internal interface IAhjoApiReader
    {
        Task<List<AhjoMeetingData>> GetMeetingsData();
    }

    internal class AhjoApiReader : IAhjoApiReader
    {
        private readonly IAhjoApiClient _ahjoApiClient;
        private readonly ILogger<AhjoApiReader> _logger;

        public AhjoApiReader(IAhjoApiClient ahjoApiClient, ILogger<AhjoApiReader> logger)
        {
            _ahjoApiClient = ahjoApiClient;
            this._logger = logger;
        }

        public async Task<List<AhjoMeetingData>> GetMeetingsData()
        {
            _logger.LogInformation("Executing GetMeetingsData()");
            var meetings = await _ahjoApiClient.GetMeetings();
            if (meetings == null)
            {
                return new List<AhjoMeetingData>();
            }

            var result = new List<AhjoMeetingData>();
            foreach (var meeting in meetings)
            {
                AhjoAgendaItemDTO[]? agendas = null;
                AhjoFullDecisionDTO[]? decisions = null;

                if (meeting.AgendaPublished == true)
                {
                    var fullMeeting = await _ahjoApiClient.GetMeetingDetails(meeting);
                    agendas = fullMeeting?.Agenda;
                }

                if (meeting.MinutesPublished == true)
                {
                    decisions = await _ahjoApiClient.GetDecisions(meeting.MeetingID);
                }

                result.Add(new AhjoMeetingData(meeting, agendas, decisions));

            }
            return result;
        }
    }
}

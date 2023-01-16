using AhjoApiService.AhjoApi.DTOs;
using AhjoApiService.AhjoApi.Models;
using Microsoft.Extensions.Logging;

namespace AhjoApiService.AhjoApi
{
    internal interface IAhjoApiReader
    {
        Task<List<AhjoMeetingData>> GetMeetingsData(DateTime startDate, DateTime emdDate);
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

        public async Task<List<AhjoMeetingData>> GetMeetingsData(DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation($"GetMeetingsData() {startDate} - {endDate}");

            var meetings = await _ahjoApiClient.GetMeetings(startDate, endDate);
            if (meetings == null)
            {
                return new List<AhjoMeetingData>();
            }

            var result = new List<AhjoMeetingData>();
            foreach (var meeting in meetings)
            {
                AhjoFullDecisionDTO[]? decisions = null;

                var fullMeeting = await _ahjoApiClient.GetMeetingDetails(meeting);
                
                if (meeting.MinutesPublished == true)
                {
                    decisions = await _ahjoApiClient.GetDecisions(meeting.MeetingID);
                }

                result.Add(new AhjoMeetingData(fullMeeting, decisions));

            }
            return result;
        }
    }
}

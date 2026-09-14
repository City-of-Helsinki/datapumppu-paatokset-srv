using AhjoApiService.AhjoApi.DTOs;
using AhjoApiService.AhjoApi.Models;
using Microsoft.Extensions.Logging;

namespace AhjoApiService.AhjoApi
{
    /// <summary>
    /// Defines the high-level orchestration contract for fetching composed meeting data from the Ahjo API.
    /// </summary>
    internal interface IAhjoApiReader
    {
        /// <summary>
        /// Fetches meetings in the given date range and enriches each one with either
        /// full agenda items (draft) or approved decisions (minutes published).
        /// </summary>
        /// <param name="startDate">Start of the date range.</param>
        /// <param name="emdDate">End of the date range.</param>
        /// <returns>A list of <see cref="AhjoMeetingData"/> objects containing the full meeting and its decisions.</returns>
        Task<List<AhjoMeetingData>> GetMeetingsData(DateTime startDate, DateTime emdDate);
    }

    /// <summary>
    /// Orchestrates meeting data collection from the Ahjo API. For each meeting in the requested
    /// date range, fetches full details and either full agenda items or approved decisions depending
    /// on whether the meeting minutes have been published.
    /// </summary>
    internal class AhjoApiReader : IAhjoApiReader
    {
        private readonly IAhjoApiClient _ahjoApiClient;
        private readonly ILogger<AhjoApiReader> _logger;

        /// <summary>
        /// Initialises a new instance of <see cref="AhjoApiReader"/>.
        /// </summary>
        /// <param name="ahjoApiClient">Client for making Ahjo API HTTP requests.</param>
        /// <param name="logger">Logger instance.</param>
        public AhjoApiReader(IAhjoApiClient ahjoApiClient, ILogger<AhjoApiReader> logger)
        {
            _ahjoApiClient = ahjoApiClient;
            this._logger = logger;
        }

        /// <inheritdoc />
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
                if (fullMeeting == null)
                {
                    continue;
                }
                
                if (meeting.MinutesPublished == true)
                {
                    // after minutes are published, we do not want to use agenda anymore
                    fullMeeting.Agenda = null;
                    decisions = await _ahjoApiClient.GetDecisions(meeting.MeetingID);
                }
                else
                {
                    var fullAgenda = await _ahjoApiClient.GetFullAgenda(fullMeeting);
                    fullMeeting.Agenda = fullAgenda;
                }

                result.Add(new AhjoMeetingData(fullMeeting, decisions));

            }
            return result;
        }
    }
}

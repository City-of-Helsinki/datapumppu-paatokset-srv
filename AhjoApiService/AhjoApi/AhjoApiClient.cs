using AhjoApiService.AhjoApi.DTOs;
using Newtonsoft.Json;

namespace AhjoApiService.AhjoApi
{
    /// <summary>
    /// HTTP client implementation for the Ahjo API. Fetches meetings, meeting details,
    /// decisions, and agenda items from the City of Helsinki Ahjo proxy endpoints.
    /// All public methods catch exceptions and return <c>null</c> or empty arrays on failure.
    /// </summary>
    internal class AhjoApiClient : IAhjoApiClient
    {
        /// <summary>
        /// The hardcoded decision maker ID for City of Helsinki ("02900").
        /// This value is passed as a query-string filter to the Ahjo meetings endpoint.
        /// </summary>
        private const string DefaultDecisionMaker = "02900";

        private readonly ILogger<AhjoApiClient> _logger;
        private readonly IAhjoApiConnection _ahjoApiConnection;

        /// <summary>
        /// Initialises a new instance of <see cref="AhjoApiClient"/>.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="ahjoApiConnection">Factory for creating authenticated HTTP connections to the Ahjo API.</param>
        public AhjoApiClient(ILogger<AhjoApiClient> logger,
            IAhjoApiConnection ahjoApiConnection)
        {
            _logger = logger;
            _ahjoApiConnection = ahjoApiConnection;
        }

        /// <inheritdoc />
        public async Task<AhjoMeetingDTO[]?> GetMeetings(DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation("Executing GetMeetings()");

            try
            {
                using var client = _ahjoApiConnection.CreateConnection();

                var query = GetMeetingsQueryParams(10, startDate, endDate);
                var apiResponse = await client.GetAsync($"/ahjo-proxy/meetings?{query}");

                var str = await apiResponse.Content.ReadAsStringAsync();
                _logger.LogInformation("meeting data {0}", str);

                var meetings = await apiResponse.Content.ReadFromJsonAsync<AhjoMeetingListDTO>();
                return meetings?.Meetings;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch meeting data");
                return null;
            }
        }

        /// <inheritdoc />
        public async Task<AhjoFullMeetingDTO?> GetMeetingDetails(AhjoMeetingDTO meetingDTO)
        {
            try
            {
                using var client = _ahjoApiConnection.CreateConnection();
                var apiResponse = await client.GetAsync($"/ahjo-proxy/meetings/single/{meetingDTO.MeetingID}");

                var str = await apiResponse.Content.ReadAsStringAsync();
                _logger.LogInformation("meeting details data {0}", str);

                var meetings = await apiResponse.Content.ReadFromJsonAsync<AhjoFullMeetingListDTO>();

                return meetings?.Meetings?.FirstOrDefault();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch meeting details data");
                return null;
            }
        }

        /// <inheritdoc />
        public async Task<AhjoFullDecisionDTO[]> GetDecisions(string meetingID)
        {
            _logger.LogInformation($"Executing GetDecisions() for meeting {meetingID}");

            try
            {
                using var client = _ahjoApiConnection.CreateConnection();
                var apiResponse = await client.GetAsync($"/ahjo-proxy/decisions?meeting_id={meetingID}");
                var decisions = await apiResponse.Content.ReadFromJsonAsync<AhjoDecisionsListDTO>();
                if (decisions == null  || decisions.Decisions == null)
                {
                    return new AhjoFullDecisionDTO[0];
                }

                var result = new List<AhjoFullDecisionDTO>();
                foreach (var decision in decisions.Decisions)
                {
                    var fullDecision = await GetDecisionDetails(decision);
                    if (fullDecision != null)
                    {
                        result.Add(fullDecision);
                    }
                }
                return result.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch decisions data");
                return new AhjoFullDecisionDTO[0];
            }
        }

        /// <inheritdoc />
        public async Task<AhjoAgendaItemDTO[]> GetFullAgenda(AhjoFullMeetingDTO meetingDTO)
        {
            try
            {
                _logger.LogInformation($"Executing GetFullAgenda() for meeting {meetingDTO.MeetingID}");
                
                var result = new List<AhjoAgendaItemDTO>();
                foreach (var agendaItem in meetingDTO.Agenda)
                {
                    var fullAgendaItem = await GetAgendaItem(meetingDTO.MeetingID, agendaItem.Pdf?.NativeId);
                    if (fullAgendaItem != null)
                    {
                        result.Add(fullAgendaItem.AgendaItem);
                    }                    
                }

                _logger.LogInformation($"fullAgenda() ready for meeting {meetingDTO.MeetingID}");
                return result.ToArray();
            } 
            catch (Exception exception)
            {
                _logger.LogInformation($"GetFullAgenda() error {exception}");
                return new AhjoAgendaItemDTO[0];
            }
        }

        /// <summary>
        /// Fetches the full agenda item details for the given meeting and native ID.
        /// Uses Newtonsoft.Json for deserialisation due to the <c>agenda_item</c> JSON property mapping.
        /// </summary>
        /// <param name="meetingId">The Ahjo meeting identifier.</param>
        /// <param name="nativeId">The native document identifier of the agenda item.</param>
        /// <returns>The full agenda item wrapper, or <c>null</c> on failure.</returns>
        private async Task<AhjoFullAgendaItemDTO?> GetAgendaItem(string? meetingId, string? nativeId)
        {
            try
            {
                using var client = _ahjoApiConnection.CreateConnection();
                var apiResponse = await client.GetAsync($"/ahjo-proxy/agenda-item/{meetingId}/{nativeId}");
                var str = await apiResponse.Content.ReadAsStringAsync();

                _logger.LogInformation($"fullAgenda() item for meeting {meetingId}/{nativeId}: {str}");
                var fullAgendaItem = JsonConvert.DeserializeObject<AhjoFullAgendaItemDTO>(str);
                return fullAgendaItem;
            }
            catch (Exception exception)
            {
                _logger.LogInformation($"GetAgendaItem() error {exception}");
                return null;
            }
        }

        /// <summary>
        /// Fetches the full details of a single decision from the Ahjo API.
        /// </summary>
        /// <param name="decisionDTO">The decision summary containing the <see cref="AhjoDecisionDTO.NativeId"/> to look up.</param>
        /// <returns>The full decision DTO, or <c>null</c> if the request fails or returns no data.</returns>
        private async Task<AhjoFullDecisionDTO?> GetDecisionDetails(AhjoDecisionDTO decisionDTO)
        {
            _logger.LogInformation($"Executing GetDecisionDetails() for decision {decisionDTO.NativeId}");
            using var client = _ahjoApiConnection.CreateConnection();
            var apiResponse = await client.GetAsync($"/ahjo-proxy/decisions/single/{decisionDTO.NativeId}");
            var decisions = await apiResponse.Content.ReadFromJsonAsync<AhjoFullDecisionListDTO>();
            
            return decisions?.Decisions.FirstOrDefault();
        }

        /// <summary>
        /// Builds the query-string parameters for the Ahjo meetings endpoint.
        /// Includes the hardcoded decision maker ID <see cref="DefaultDecisionMaker"/> ("02900" — City of Helsinki).
        /// </summary>
        /// <param name="maxCount">Maximum number of meetings to return.</param>
        /// <param name="startDate">Start of the date range.</param>
        /// <param name="endDate">End of the date range.</param>
        /// <returns>A URL-encoded query string (without leading <c>?</c>).</returns>
        private string GetMeetingsQueryParams(int maxCount, DateTime startDate, DateTime endDate)
        {
            
            var startDateStr = startDate.ToString("yyyy-MM-ddTHH':'mm':'ss");
            var endDateStr = endDate.ToString("yyyy-MM-ddTHH':'mm':'ss");
            return $"start={startDateStr}&end={endDateStr}&decisionmaker_id={DefaultDecisionMaker}&size={maxCount}&agendaminutespublished=true";
        }
    }
}

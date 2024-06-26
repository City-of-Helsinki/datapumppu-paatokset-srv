﻿using AhjoApiService.AhjoApi.DTOs;
using Newtonsoft.Json;

namespace AhjoApiService.AhjoApi
{
    internal class AhjoApiClient : IAhjoApiClient
    {
        private const string DefaultDecisionMaker = "02900";

        private readonly ILogger<AhjoApiClient> _logger;
        private readonly IAhjoApiConnection _ahjoApiConnection;

        public AhjoApiClient(ILogger<AhjoApiClient> logger,
            IAhjoApiConnection ahjoApiConnection)
        {
            _logger = logger;
            _ahjoApiConnection = ahjoApiConnection;
        }

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

        private async Task<AhjoFullDecisionDTO?> GetDecisionDetails(AhjoDecisionDTO decisionDTO)
        {
            _logger.LogInformation($"Executing GetDecisionDetails() for decision {decisionDTO.NativeId}");
            using var client = _ahjoApiConnection.CreateConnection();
            var apiResponse = await client.GetAsync($"/ahjo-proxy/decisions/single/{decisionDTO.NativeId}");
            var decisions = await apiResponse.Content.ReadFromJsonAsync<AhjoFullDecisionListDTO>();
            
            return decisions?.Decisions.FirstOrDefault();
        }

        private string GetMeetingsQueryParams(int maxCount, DateTime startDate, DateTime endDate)
        {
            
            var startDateStr = startDate.ToString("yyyy-MM-ddTHH':'mm':'ss");
            var endDateStr = endDate.ToString("yyyy-MM-ddTHH':'mm':'ss");
            return $"start={startDateStr}&end={endDateStr}&decisionmaker_id={DefaultDecisionMaker}&size={maxCount}&agendaminutespublished=true";
        }
    }
}

﻿using AhjoApiService.AhjoApi.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace AhjoApiService.AhjoApi
{
    internal class AhjoApiClient : IAhjoApiClient
    {
        private const string DefaultDecisionMaker = "02900";

        private readonly ILogger<AhjoApiClient> _logger;
        private readonly IConfiguration _configuration;

        public AhjoApiClient(ILogger<AhjoApiClient> logger,
            IConfiguration configuration)
        {
            this._logger = logger;
            _configuration = configuration;
        }

        public async Task<AhjoMeetingDTO[]?> GetMeetings(DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation("Executing GetMeetings()");

            try
            {
                using var client = CreateClient();

                var query = GetMeetingsQueryParams(10, startDate, endDate);
                var apiResponse = await client.GetAsync($"/ahjo-proxy/meetings?{query}");

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
                using var client = CreateClient();
                var apiResponse = await client.GetAsync($"/ahjo-proxy/meetings/single/{meetingDTO.MeetingID}");

                var str = await apiResponse.Content.ReadAsStringAsync();
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
                using var client = CreateClient();
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

        private async Task<AhjoFullDecisionDTO?> GetDecisionDetails(AhjoDecisionDTO decisionDTO)
        {
            _logger.LogInformation($"Executing GetDecisionDetails() for decision {decisionDTO.NativeId}");
            using var client = CreateClient();
            var apiResponse = await client.GetAsync($"/ahjo-proxy/decisions/single/{decisionDTO.NativeId}");
            var decisions = await apiResponse.Content.ReadFromJsonAsync<AhjoFullDecisionListDTO>();
            
            return decisions?.Decisions.FirstOrDefault();
        }

        private HttpClient CreateClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_configuration["AHJO_API_URL"]);

            var apiKey = _configuration["AHJO_API_KEY"];
            httpClient.DefaultRequestHeaders.Add("api-key", apiKey);

            return httpClient;
        }

        private string GetMeetingsQueryParams(int maxCount, DateTime startDate, DateTime endDate)
        {
            
            var startDateStr = startDate.ToString("yyyy-MM-ddTHH':'mm':'ss");
            var endDateStr = endDate.ToString("yyyy-MM-ddTHH':'mm':'ss");
            return $"start={startDateStr}&end={endDateStr}&decisionmaker_id={DefaultDecisionMaker}&size={maxCount}&agendaminutespublished=true";
        }
    }
}

using AhjoApiService.AhjoApi.DTOs;
using AhjoApiService.AhjoApi.Models;
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

        public async Task<AhjoMeetingDTO[]?> GetMeetings()
        {
            _logger.LogInformation("Executing GetMeetings()");
            using var client = CreateClient();

            var query = GetMeetingsQueryParams(10);
            var apiResponse = await client.GetAsync($"/ahjo-proxy/meetings?{query}");

            var meetings = await apiResponse.Content.ReadFromJsonAsync<AhjoMeetingListDTO>();
            return meetings?.Meetings;
        }

        public async Task<AhjoFullMeetingDTO?> GetMeetingDetails(AhjoMeetingDTO meetingDTO)
        {
            using var client = CreateClient();
            var apiResponse = await client.GetAsync($"/ahjo-proxy/meetings/single/{meetingDTO.MeetingID}");

            var str = await apiResponse.Content.ReadAsStringAsync();
            var meetings = await apiResponse.Content.ReadFromJsonAsync<AhjoFullMeetingListDTO>();

            return meetings?.Meetings?.FirstOrDefault();
        }

        public async Task<AhjoDecisionData[]> GetDecisions(string meetingID)
        {
            _logger.LogInformation($"Executing GetDecisions() for meeting {meetingID}");
            using var client = CreateClient();
            var apiResponse = await client.GetAsync($"/ahjo-proxy/decisions?meeting_id={meetingID}");
            var decisions = await apiResponse.Content.ReadFromJsonAsync<AhjoDecisionsListDTO>();
            var result = new List<AhjoDecisionData>();

            foreach (var decision in decisions.Decisions)
            {
                var fullDecision = await GetDecisionDetails(decision);
                var decisionData = new AhjoDecisionData()
                {
                    MeetingID = meetingID,
                    Decision = fullDecision
                };
                result.Add(decisionData);
            }
            return result.ToArray();
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
            httpClient.BaseAddress = new Uri(_configuration["AhjoApi:url"]);

            var apiKey = _configuration["AhjoApi:api-key"];
            httpClient.DefaultRequestHeaders.Add("api-key", apiKey);

            return httpClient;
        }

        private string GetMeetingsQueryParams(int maxCount)
        {
            var startDate = DateTime.Now.AddMonths(-1);
            var startDateStr = startDate.ToString("yyyy-MM-ddTHH':'mm':'ss");
            return $"start={startDateStr}&decisionmaker_id={DefaultDecisionMaker}&size={maxCount}&agendaminutespublished=true";
        }
    }
}

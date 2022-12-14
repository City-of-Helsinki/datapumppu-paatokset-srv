using AhjoApiService.AhjoApi.DTOs;
using AhjoApiService.AhjoApi.Models;

namespace AhjoApiService.AhjoApi
{
    internal interface IAhjoApiClient
    {
        Task<AhjoMeetingDTO[]?> GetMeetings();

        Task<AhjoFullMeetingDTO?> GetMeetingDetails(AhjoMeetingDTO meetingDTO);

        Task<AhjoDecisionData[]> GetDecisions(string meetingID);
    }
}

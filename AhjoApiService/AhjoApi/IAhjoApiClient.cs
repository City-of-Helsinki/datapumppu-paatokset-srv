using AhjoApiService.AhjoApi.DTOs;

namespace AhjoApiService.AhjoApi
{
    internal interface IAhjoApiClient
    {
        Task<AhjoMeetingDTO[]?> GetMeetings(DateTime startDate);

        Task<AhjoFullMeetingDTO?> GetMeetingDetails(AhjoMeetingDTO meetingDTO);

        Task<AhjoFullDecisionDTO[]> GetDecisions(string meetingID);
    }
}

using AhjoApiService.AhjoApi.DTOs;

namespace AhjoApiService.AhjoApi
{
    internal interface IAhjoApiClient
    {
        Task<AhjoMeetingDTO[]?> GetMeetings(DateTime startDate, DateTime endDate);

        Task<AhjoFullMeetingDTO?> GetMeetingDetails(AhjoMeetingDTO meetingDTO);

        Task<AhjoFullDecisionDTO[]> GetDecisions(string meetingID);

        Task<AhjoAgendaItemDTO[]> GetFullAgenda(AhjoFullMeetingDTO meetingDTO);
    }
}

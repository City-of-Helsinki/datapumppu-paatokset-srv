using AhjoApiService.AhjoApi.DTOs;

namespace AhjoApiService.AhjoApi.Models
{
    internal class AhjoMeetingData
    {
        public AhjoFullMeetingDTO FullMeeting { get; set; }

        public AhjoFullDecisionDTO[] Decisions { get; set; }

        public AhjoMeetingData(AhjoFullMeetingDTO fullMeeting, AhjoFullDecisionDTO[]? decisions = null)
        {
            FullMeeting = fullMeeting;
            Decisions = decisions ?? Array.Empty<AhjoFullDecisionDTO>();
        }
    }
}

using AhjoApiService.AhjoApi.DTOs;

namespace AhjoApiService.AhjoApi.Models
{
    internal class AhjoMeetingData
    {
        public AhjoFullMeetingDTO FullMeeting { get; set; }

        public AhjoDecisionData[] Decisions { get; set; }

        public AhjoMeetingData(AhjoFullMeetingDTO fullMeeting, AhjoDecisionData[]? decisions = null)
        {
            FullMeeting = fullMeeting;
            Decisions = decisions ?? Array.Empty<AhjoDecisionData>();
        }
    }
}

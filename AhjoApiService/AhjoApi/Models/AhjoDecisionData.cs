using AhjoApiService.AhjoApi.DTOs;

namespace AhjoApiService.AhjoApi.Models
{
    internal class AhjoDecisionData
    {
        public string MeetingID { get; set; }
        public AhjoFullDecisionDTO Decision { get; set; }
    }
}

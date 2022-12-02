namespace AhjoApiService.AhjoApi.DTOs
{
    internal class AhjoMeetingDTO
    {
        public DateTime? DateMeeting { get; set; }

        public string? MeetingID { get; set; }

        public string? DecisionMaker { get; set; }

        public string? DecisionMakerID { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }

        public bool? AgendaPublished { get; set; }

        public bool? MinutesPublished { get; set; }
    }
}

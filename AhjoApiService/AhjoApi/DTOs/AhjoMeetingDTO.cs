namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Represents a meeting summary returned by the Ahjo API meetings list endpoint.
    /// </summary>
    internal class AhjoMeetingDTO
    {
        /// <summary>Date and time of the meeting.</summary>
        public DateTime? DateMeeting { get; set; }

        /// <summary>Unique Ahjo meeting identifier.</summary>
        public string? MeetingID { get; set; }

        /// <summary>Name of the decision-making body.</summary>
        public string? DecisionMaker { get; set; }

        /// <summary>Identifier of the decision-making body (e.g. "02900").</summary>
        public string? DecisionMakerID { get; set; }

        /// <summary>Display name of the meeting.</summary>
        public string? Name { get; set; }

        /// <summary>Physical or virtual location where the meeting is held.</summary>
        public string? Location { get; set; }

        /// <summary>Whether the meeting agenda has been published.</summary>
        public bool? AgendaPublished { get; set; }

        /// <summary>Whether the meeting minutes have been published. When <c>true</c>, approved decisions are available instead of draft agenda items.</summary>
        public bool? MinutesPublished { get; set; }
    }
}

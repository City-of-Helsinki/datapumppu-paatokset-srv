namespace AhjoApiService.StorageClient.DTOs
{
    /// <summary>
    /// Represents a meeting payload sent to the storage API.
    /// Mapped from <see cref="AhjoApi.DTOs.AhjoFullMeetingDTO"/>.
    /// </summary>
    internal class StorageMeetingDTO
    {
        /// <summary>Date and time of the meeting. Mapped from <c>DateMeeting</c>.</summary>
        public DateTime? MeetingDate { get; set; }

        /// <summary>Unique Ahjo meeting identifier.</summary>
        public string? MeetingID { get; set; }

        /// <summary>Display name of the meeting.</summary>
        public string? Name { get; set; }

        /// <summary>Physical or virtual location of the meeting.</summary>
        public string? Location { get; set; }

        /// <summary>Sequential number of the meeting in the series.</summary>
        public int? MeetingSequenceNumber { get; set; }

        /// <summary>Agenda items for the meeting (populated when minutes are NOT published).</summary>
        public List<StorageAgendaItemDTO>? Agendas { get; set; }

        /// <summary>Decisions for the meeting (populated when minutes ARE published).</summary>
        public List<StorageDecisionDTO>? Decisions { get; set; }
    }
}

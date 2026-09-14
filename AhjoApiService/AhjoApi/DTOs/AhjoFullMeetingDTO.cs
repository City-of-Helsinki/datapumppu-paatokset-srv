using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Represents the full details of a meeting returned by the Ahjo single-meeting endpoint.
    /// Extends the basic summary with agenda items, composition, documents, and navigation links.
    /// </summary>
    internal class AhjoFullMeetingDTO
    {
        /// <summary>Date and time of the meeting.</summary>
        public DateTime? DateMeeting { get; set; }

        /// <summary>Unique Ahjo meeting identifier.</summary>
        public string? MeetingID { get; set; }

        /// <summary>Name of the decision-making body.</summary>
        public string? DecisionMaker { get; set; }

        /// <summary>Identifier of the decision-making body.</summary>
        public string? DecisionMakerID { get; set; }

        /// <summary>Display name of the meeting.</summary>
        public string? Name { get; set; }

        /// <summary>Physical or virtual location of the meeting.</summary>
        public string? Location { get; set; }

        /// <summary>Whether the meeting agenda has been published.</summary>
        public bool? AgendaPublished { get; set; }

        /// <summary>Whether the meeting minutes have been published.</summary>
        public bool? MinutesPublished { get; set; }

        /// <summary>Agenda items for the meeting. Set to <c>null</c> when minutes are published.</summary>
        public AhjoAgendaItemDTO[]? Agenda { get; set; }

        /// <summary>Composition of the decision-making body (members, roles, deputies).</summary>
        public AhjoCompositionItemDTO[]? Composition { get; set; }

        /// <summary>Sequential number of this meeting in the meeting series.</summary>
        public int MeetingSequenceNumber { get; set; }

        /// <summary>Reference to the previous meeting in the series.</summary>
        public AhjoMeetingDTO? PreviousMeeting { get; set; }

        /// <summary>Reference to the next meeting in the series.</summary>
        public AhjoMeetingDTO? NextMeeting { get; set; }

        /// <summary>Current status of the meeting (e.g. scheduled, completed).</summary>
        public string? Status { get; set; }

        /// <summary>Decision announcement text.</summary>
        public string? DecisionAnnouncement { get; set; }

        /// <summary>Information about attachments that have not been published.</summary>
        public string? AttachmentsNotPublishedInfo { get; set; }

        /// <summary>Public documents associated with the meeting.</summary>
        public AhjoRecordPublicDTO[]? MeetingDocuments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhjoApiService.AhjoApi.DTOs
{
    internal class AhjoFullMeetingDTO
    {
        public DateTime? DateMeeting { get; set; }

        public string? MeetingID { get; set; }

        public string? DecisionMaker { get; set; }

        public string? DecisionMakerID { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }

        public bool? AgendaPublished { get; set; }

        public bool? MinutesPublished { get; set; }

        public AhjoAgendaItemDTO[]? Agenda { get; set; }

        public AhjoCompositionItemDTO[]? Composition { get; set; }

        public int MeetingSequenceNumber { get; set; }

        public AhjoMeetingDTO? PreviousMeeting { get; set; }

        public AhjoMeetingDTO? NextMeeting { get; set; }

        public string? Status { get; set; }

        public string? DecisionAnnouncement { get; set; }

        public string? AttachmentsNotPublishedInfo { get; set; }

        public AhjoRecordPublicDTO[]? MeetingDocuments { get; set; }
    }
}

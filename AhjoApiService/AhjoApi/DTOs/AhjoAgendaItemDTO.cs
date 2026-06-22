namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Represents a single agenda item within a meeting, including section, content, and associated attachments.
    /// </summary>
    internal class AhjoAgendaItemDTO
    {
        /// <summary>Sequential point number on the agenda.</summary>
        public int AgendaPoint { get; set; }

        /// <summary>Section identifier (e.g. paragraph number).</summary>
        public string? Section { get; set; }

        /// <summary>Title of the agenda item. Mapped to <c>Title</c> in the storage DTO.</summary>
        public string? AgendaItem { get; set; }

        /// <summary>Display label for the case identifier.</summary>
        public string? CaseIDLabel { get; set; }

        /// <summary>HTML content of the agenda item.</summary>
        public string? Html { get; set; }

        /// <summary>HTML content of the decision history for this agenda item.</summary>
        public string? DecisionHistoryHTML { get; set; }

        /// <summary>PDF attachment for the agenda item. Its <c>Language</c> field is used to derive the item language.</summary>
        public AhjoAttachmentDTO? Pdf { get; set; }

        /// <summary>PDF attachment containing the decision history.</summary>
        public AhjoAttachmentDTO? DecisionHistoryPdf { get; set; }

        /// <summary>Additional file attachments associated with the agenda item.</summary>
        public AhjoAttachmentDTO[]? Attachments { get; set; }
    }
}

namespace AhjoApiService.StorageClient.DTOs
{
    /// <summary>
    /// Represents an agenda item payload sent to the storage API.
    /// Mapped from <see cref="AhjoApi.DTOs.AhjoAgendaItemDTO"/>.
    /// </summary>
    internal class StorageAgendaItemDTO
    {
        /// <summary>Sequential point number on the agenda.</summary>
        public int AgendaPoint { get; set; }

        /// <summary>Section identifier.</summary>
        public string? Section { get; set; }

        /// <summary>Title of the agenda item. Mapped from <c>AgendaItem</c> in the Ahjo DTO.</summary>
        public string? Title { get; set; }

        /// <summary>Display label for the case identifier.</summary>
        public string? CaseIDLabel { get; set; }

        /// <summary>HTML content of the agenda item.</summary>
        public string? Html { get; set; }

        /// <summary>Language code derived from the PDF attachment metadata.</summary>
        public string? Language { get; set; }

        /// <summary>HTML content of the decision history.</summary>
        public string? DecisionHistoryHTML { get; set; }

        /// <summary>PDF attachment for the agenda item.</summary>
        public StorageAttachmentDTO? Pdf { get; set; }

        /// <summary>Additional file attachments. Titles are truncated to 256 characters.</summary>
        public StorageAttachmentDTO[]? Attachments { get; set; }
    }
}

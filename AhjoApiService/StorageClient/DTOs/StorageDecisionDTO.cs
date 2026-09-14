namespace AhjoApiService.StorageClient.DTOs
{
    /// <summary>
    /// Represents a decision payload sent to the storage API.
    /// Mapped from <see cref="AhjoApi.DTOs.AhjoFullDecisionDTO"/>.
    /// </summary>
    internal class StorageDecisionDTO
    {
        /// <summary>Native document identifier.</summary>
        public string? NativeId { get; set; }

        /// <summary>Title of the decision.</summary>
        public string? Title { get; set; }

        /// <summary>Display label for the case identifier.</summary>
        public string? CaseIDLabel { get; set; }

        /// <summary>Case identifier.</summary>
        public string? CaseID { get; set; }

        /// <summary>Section number within the meeting agenda.</summary>
        public string? Section { get; set; }

        /// <summary>HTML content of the decision. Mapped from <c>Content</c> in the Ahjo DTO.</summary>
        public string? Html { get; set; }

        /// <summary>Motion text (proposal) for the decision.</summary>
        public string? Motion { get; set; }

        /// <summary>Classification code for the decision topic.</summary>
        public string? ClassificationCode { get; set; }

        /// <summary>Human-readable classification title.</summary>
        public string? ClassificationTitle { get; set; }

        /// <summary>Language code derived from the PDF attachment metadata.</summary>
        public string? Language { get; set; }

        /// <summary>PDF attachment for the decision.</summary>
        public StorageAttachmentDTO? Pdf { get; set; }

        /// <summary>PDF attachment for the decision history.</summary>
        public StorageAttachmentDTO? DecisionHistoryPdf { get; set; }

        /// <summary>HTML content of the decision history.</summary>
        public string? DecisionHistoryHtml { get; set; }

        /// <summary>Additional file attachments. Titles are truncated to 256 characters.</summary>
        public StorageAttachmentDTO[]? Attachments { get; set; }
    }
}

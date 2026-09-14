namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Represents the full details of a decision returned by the Ahjo single-decision endpoint.
    /// Extends the basic summary with content, motion, voting results, attachments, and decision history.
    /// </summary>
    internal class AhjoFullDecisionDTO
    {
        /// <summary>Native document identifier for the decision.</summary>
        public string NativeId { get; set; }

        /// <summary>Title of the decision.</summary>
        public string? Title { get; set; }

        /// <summary>Display label for the case identifier.</summary>
        public string? CaseIDLabel { get; set; }

        /// <summary>Case identifier associated with the decision.</summary>
        public string? CaseID { get; set; }

        /// <summary>Section number within the meeting agenda.</summary>
        public string? Section { get; set; }

        /// <summary>HTML content of the decision. Mapped to <c>Html</c> in the storage DTO.</summary>
        public string Content { get; set; }

        /// <summary>The motion text (proposal) for the decision.</summary>
        public string? Motion { get; set; }

        /// <summary>Classification code for the decision topic.</summary>
        public string? ClassificationCode { get; set; }

        /// <summary>Human-readable classification title.</summary>
        public string? ClassificationTitle { get; set; }

        /// <summary>The organisation responsible for the decision.</summary>
        public AhjoFullOrganizationDTO? Organization { get; set; }

        /// <summary>Reference to the meeting where this decision was made.</summary>
        public AhjoMeetingDTO? Meeting { get; set; }

        /// <summary>Voting results (ayes, noes, blank, absent) if a vote was held.</summary>
        public AhjoVotingResultDTO[]? VotingResults { get; set; }

        /// <summary>File attachments associated with the decision.</summary>
        public AhjoAttachmentDTO[]? Attachments { get; set; }

        /// <summary>Previous decisions in the decision chain for this case.</summary>
        public AhjoDecisionDTO[]? PreviousDecisions { get; set; }

        /// <summary>PDF attachment for the decision. Its <c>Language</c> field is used to derive the decision language.</summary>
        public AhjoAttachmentDTO? Pdf { get; set; }

        /// <summary>Date when the decision was made.</summary>
        public DateTime? DateDecision { get; set; }

        /// <summary>PDF attachment containing the decision history.</summary>
        public AhjoAttachmentDTO? DecisionHistoryPdf { get; set; }

        /// <summary>HTML content of the decision history.</summary>
        public string? DecisionHistoryHtml { get; set; }
    }
}

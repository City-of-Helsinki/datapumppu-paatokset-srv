namespace AhjoApiService.AhjoApi.DTOs
{
    internal class AhjoFullDecisionDTO
    {
        public string NativeId { get; set; }

        public string? Title { get; set; }

        public string? CaseIDLabel { get; set; }

        public string? CaseID { get; set; }

        public string? Section { get; set; }

        public string Content { get; set; }

        public string? Motion { get; set; }

        public string? ClassificationCode { get; set; }

        public string? ClassificationTitle { get; set; }

        public AhjoFullOrganizationDTO? Organization { get; set; }

        public AhjoMeetingDTO? Meeting { get; set; }

        public AhjoVotingResultDTO[]? VotingResults { get; set; }

        public AhjoDecisionAttachmentDTO[]? Attachments { get; set; }

        public AhjoDecisionDTO[]? PreviousDecisions { get; set; }

        public AhjoDecisionAttachmentDTO? Pdf { get; set; }

        public DateTime? DateDecision { get; set; }

        public AhjoDecisionAttachmentDTO? DecisionHistoryPdf { get; set; }

        public string? DecisionHistoryHtml { get; set; }
    }
}

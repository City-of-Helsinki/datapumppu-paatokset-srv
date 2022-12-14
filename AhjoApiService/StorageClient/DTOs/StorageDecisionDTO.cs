namespace AhjoApiService.StorageClient.DTOs
{
    internal class StorageDecisionDTO
    {
        public string? MeetingID { get; set; }

        public string? NativeId { get; set; }

        public string? Title { get; set; }

        public string? CaseIDLabel { get; set; }

        public string? CaseID { get; set; }

        public string? Section { get; set; }

        public string? Html { get; set; }

        public string? Motion { get; set; }

        public string? ClassificationCode { get; set; }

        public string? ClassificationTitle { get; set; }

        public StorageDecisionAttachmentDTO? Pdf { get; set; }

        public StorageDecisionAttachmentDTO? DecisionHistoryPdf { get; set; }

        public string? DecisionHistoryHtml { get; set; }

        public StorageDecisionAttachmentDTO[]? Attachments { get; set; }
    }
}

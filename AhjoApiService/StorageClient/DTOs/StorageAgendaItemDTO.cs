namespace AhjoApiService.StorageClient.DTOs
{
    internal class StorageAgendaItemDTO
    {
        public int AgendaPoint { get; set; }

        public string? Section { get; set; }

        public string? Title { get; set; }

        public string? CaseIDLabel { get; set; }

        public string? Html { get; set; }

        public string? Language { get; set; }

        public string? DecisionHistoryHTML { get; set; }

        public StorageAttachmentDTO? Pdf { get; set; }

        public StorageAttachmentDTO[]? Attachments { get; set; }
    }
}

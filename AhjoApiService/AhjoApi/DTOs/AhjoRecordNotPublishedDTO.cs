namespace AhjoApiService.AhjoApi.DTOs
{
    internal class AhjoRecordNotPublishedDTO
    {
        public string? Title { get; set; }

        public string? AttachmentNumber { get; set; }

        public string? PublicityClass { get; set; }

        public string[]? SecurityReasons { get; set; }
    }
}

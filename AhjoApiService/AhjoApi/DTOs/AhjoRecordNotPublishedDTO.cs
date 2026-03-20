namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Represents a meeting document that has not been published, containing only metadata about the restriction.
    /// </summary>
    internal class AhjoRecordNotPublishedDTO
    {
        /// <summary>Title of the unpublished document.</summary>
        public string? Title { get; set; }

        /// <summary>Sequential attachment number.</summary>
        public string? AttachmentNumber { get; set; }

        /// <summary>Publicity class indicating why the document is not public.</summary>
        public string? PublicityClass { get; set; }

        /// <summary>Reasons for restricting access to the document.</summary>
        public string[]? SecurityReasons { get; set; }
    }
}

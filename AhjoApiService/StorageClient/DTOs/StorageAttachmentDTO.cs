namespace AhjoApiService.StorageClient.DTOs
{
    /// <summary>
    /// Represents a file attachment payload sent to the storage API.
    /// Mapped from <see cref="AhjoApi.DTOs.AhjoAttachmentDTO"/>.
    /// </summary>
    internal class StorageAttachmentDTO
    {
        /// <summary>Native document identifier.</summary>
        public string? NativeId { get; set; }

        /// <summary>Display title. Truncated to 256 characters during mapping.</summary>
        public string? Title { get; set; }

        /// <summary>Sequential attachment number.</summary>
        public string? AttachmentNumber { get; set; }

        /// <summary>Publicity class of the attachment.</summary>
        public string? PublicityClass { get; set; }

        /// <summary>Reasons for restricting access.</summary>
        public string[]? SecurityReasons { get; set; }

        /// <summary>MIME type or document type descriptor.</summary>
        public string? Type { get; set; }

        /// <summary>URI pointing to the downloadable file.</summary>
        public string? FileURI { get; set; }

        /// <summary>Language code for the attachment content.</summary>
        public string? Language { get; set; }

        /// <summary>Indicates whether the attachment contains personal data.</summary>
        public string? PersonalData { get; set; }

        /// <summary>Date when the attachment was issued/published.</summary>
        public string? Issued { get; set; }
    }
}

namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Represents a file attachment associated with a decision or agenda item in the Ahjo API.
    /// Also used for PDF metadata from which the document language is derived.
    /// </summary>
    internal class AhjoAttachmentDTO
    {
        /// <summary>Native document identifier of the attachment.</summary>
        public string? NativeId { get; set; }

        /// <summary>Display title of the attachment. Truncated to 256 characters during mapping.</summary>
        public string? Title { get; set; }

        /// <summary>Sequential attachment number within the parent decision or agenda item.</summary>
        public string? AttachmentNumber { get; set; }

        /// <summary>Publicity class of the attachment (e.g. public, secret).</summary>
        public string? PublicityClass { get; set; }

        /// <summary>Reasons for restricting access, if the attachment is not public.</summary>
        public string[]? SecurityReasons { get; set; }

        /// <summary>MIME type or document type descriptor.</summary>
        public string? Type { get; set; }

        /// <summary>URI pointing to the downloadable file.</summary>
        public string? FileURI { get; set; }

        /// <summary>Language code for the attachment content (e.g. "fi", "sv", "en").</summary>
        public string? Language { get; set; }

        /// <summary>Indicates whether the attachment contains personal data.</summary>
        public string? PersonalData { get; set; }

        /// <summary>Date when the attachment was issued/published.</summary>
        public string? Issued { get; set; }
    }
}

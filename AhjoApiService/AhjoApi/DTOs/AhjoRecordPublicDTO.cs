using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Represents a publicly available meeting document (record) from the Ahjo API.
    /// </summary>
    internal class AhjoRecordPublicDTO
    {
        /// <summary>Title of the document.</summary>
        public string? Title { get; set; }

        /// <summary>Sequential attachment number.</summary>
        public string? AttachmentNumber { get; set; }

        /// <summary>Publicity class of the document.</summary>
        public string? PublicityClass { get; set; }

        /// <summary>Reasons for restricting access, if applicable.</summary>
        public string[]? SecurityReasons { get; set; }

        /// <summary>MIME type or document type descriptor.</summary>
        public string? Type { get; set; }

        /// <summary>URI pointing to the downloadable file.</summary>
        public string? FileURI { get; set; }

        /// <summary>Language code for the document content.</summary>
        public string? Language { get; set; }

        /// <summary>Indicates whether the document contains personal data.</summary>
        public string? PersonalData { get; set; }

        /// <summary>Date when the document was issued/published.</summary>
        public string? Issued { get; set; }

        /// <summary>Native document identifier.</summary>
        public string? NativeId { get; set; }
    }
}

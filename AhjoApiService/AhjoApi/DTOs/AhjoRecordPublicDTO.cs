using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhjoApiService.AhjoApi.DTOs
{
    internal class AhjoRecordPublicDTO
    {
        public string? Title { get; set; }

        public string? AttachmentNumber { get; set; }

        public string? PublicityClass { get; set; }

        public string[]? SecurityReasons { get; set; }

        public string? Type { get; set; }

        public string? FileURI { get; set; }

        public string? Language { get; set; }

        public string? PersonalData { get; set; }

        public string? Issued { get; set; }

        public string? NativeId { get; set; }
    }
}

namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Represents an organisation (decision-making body) summary from the Ahjo API.
    /// </summary>
    internal class AhjoOrganizationDTO
    {
        /// <summary>Name of the organisation.</summary>
        public string? Name { get; set; }

        /// <summary>Unique Ahjo identifier for the organisation.</summary>
        public string? ID { get; set; }

        /// <summary>Type identifier classifying the organisation.</summary>
        public string? TypeId { get; set; }

        /// <summary>Whether the organisation currently exists (active status).</summary>
        public string? Existing { get; set; }

        /// <summary>Date when the organisation was formed.</summary>
        public DateTime? Formed { get; set; }

        /// <summary>Date when the organisation was dissolved. <c>null</c> if still active.</summary>
        public DateTime? Dissolved { get; set; }
    }
}

namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Represents the full details of an organisation from the Ahjo API,
    /// including hierarchy (parent and child organisations).
    /// </summary>
    internal class AhjoFullOrganizationDTO
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

        /// <summary>Human-readable type of the organisation (e.g. "board", "committee").</summary>
        public string? Type { get; set; }

        /// <summary>Administrative sector the organisation belongs to.</summary>
        public string? Sector { get; set; }

        /// <summary>Parent organisations in the hierarchy.</summary>
        public AhjoOrganizationListDTO? OrganizationLevelAbove { get; set; }

        /// <summary>Child organisations in the hierarchy.</summary>
        public AhjoOrganizationListDTO? OrganizationLevelBelow { get; set; }
    }
}

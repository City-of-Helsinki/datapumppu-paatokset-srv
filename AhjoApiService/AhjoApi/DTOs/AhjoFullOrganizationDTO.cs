namespace AhjoApiService.AhjoApi.DTOs
{
    internal class AhjoFullOrganizationDTO
    {
        public string? Name { get; set; }

        public string? ID { get; set; }

        public string? TypeId { get; set; }

        public string? Existing { get; set; }

        public DateTime? Formed { get; set; }

        public DateTime? Dissolved { get; set; }

        public string? Type { get; set; }

        public string? Sector { get; set; }

        public AhjoOrganizationListDTO? OrganizationLevelAbove { get; set; }

        public AhjoOrganizationListDTO? OrganizationLevelBelow { get; set; }
    }
}

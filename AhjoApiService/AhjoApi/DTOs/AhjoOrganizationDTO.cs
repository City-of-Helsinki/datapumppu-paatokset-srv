namespace AhjoApiService.AhjoApi.DTOs
{
    internal class AhjoOrganizationDTO
    {
        public string? Name { get; set; }

        public string? ID { get; set; }

        public string? TypeId { get; set; }

        public string? Existing { get; set; }

        public DateTime? Formed { get; set; }

        public DateTime? Dissolved { get; set; }
    }
}

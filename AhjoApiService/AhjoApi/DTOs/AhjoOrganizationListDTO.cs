namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Wrapper for an array of <see cref="AhjoOrganizationDTO"/> returned by the Ahjo organisations endpoint.
    /// </summary>
    internal class AhjoOrganizationListDTO
    {
        /// <summary>The array of organisation summaries.</summary>
        public AhjoOrganizationDTO[]? Organizations { get; set; }
    }
}

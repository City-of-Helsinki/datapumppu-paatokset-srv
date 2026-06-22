namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Wrapper for an array of <see cref="AhjoDecisionDTO"/> returned by the Ahjo decisions list endpoint.
    /// </summary>
    internal class AhjoDecisionsListDTO
    {
        /// <summary>The array of decision summaries.</summary>
        public AhjoDecisionDTO[]? Decisions { get; set; }
    }
}

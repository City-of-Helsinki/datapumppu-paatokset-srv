namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Wrapper for an array of <see cref="AhjoFullDecisionDTO"/> returned by the Ahjo single-decision endpoint.
    /// </summary>
    internal class AhjoFullDecisionListDTO
    {
        /// <summary>The array of full decision details.</summary>
        public AhjoFullDecisionDTO[]? Decisions { get; set; }
    }
}

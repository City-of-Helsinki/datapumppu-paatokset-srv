namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Represents a decision summary returned by the Ahjo decisions list endpoint.
    /// </summary>
    internal class AhjoDecisionDTO
    {
        /// <summary>Native document identifier for the decision.</summary>
        public string NativeId { get; set; }

        /// <summary>Title of the decision.</summary>
        public string? Title { get; set; }

        /// <summary>Display label for the case identifier.</summary>
        public string? CaseIDLabel { get; set; }

        /// <summary>Case identifier associated with the decision.</summary>
        public string? CaseID { get; set; }

        /// <summary>Section number of the decision within the meeting agenda.</summary>
        public string? Section { get; set; }
    }
}

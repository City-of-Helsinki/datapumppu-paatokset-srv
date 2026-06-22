namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Represents a single voter who participated in a vote on a decision.
    /// </summary>
    internal class AhjoVoterDTO
    {
        /// <summary>Agent identifier of the voter.</summary>
        public string? AgentId { get; set; }

        /// <summary>Unique identifier of the voter.</summary>
        public string? ID { get; set; }

        /// <summary>Full name of the voter.</summary>
        public string? Name { get; set; }

        /// <summary>Official title or position of the voter.</summary>
        public string? Title { get; set; }

        /// <summary>Name of the corporate body the voter belongs to.</summary>
        public string? CorporateName { get; set; }

        /// <summary>Council group or political party of the voter.</summary>
        public string? CouncilGroup { get; set; }
    }
}

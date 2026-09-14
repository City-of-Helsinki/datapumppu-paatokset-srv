namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Represents a group of voters who cast the same vote (e.g. all ayes, all noes).
    /// </summary>
    internal class AhjoVoterListDTO
    {
        /// <summary>Total number of votes in this category.</summary>
        public int? NumberOfVotes { get; set; }

        /// <summary>Individual voters in this category.</summary>
        public AhjoVoterDTO[]? Voters { get; set; }

        /// <summary>Textual description or summary of this voting category.</summary>
        public string? Content { get; set; }
    }
}

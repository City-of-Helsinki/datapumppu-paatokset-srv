namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Represents the voting results for a decision, broken down by vote category.
    /// </summary>
    internal class AhjoVotingResultDTO
    {
        /// <summary>Voters who voted in favour (aye).</summary>
        public AhjoVoterListDTO? Ayes { get; set; }

        /// <summary>Voters who voted against (no).</summary>
        public AhjoVoterListDTO? Noes { get; set; }

        /// <summary>Voters who cast a blank (abstain) vote.</summary>
        public AhjoVoterListDTO? Blank { get; set; }

        /// <summary>Members who were absent during the vote.</summary>
        public AhjoVoterListDTO? Absent { get; set; }
    }
}

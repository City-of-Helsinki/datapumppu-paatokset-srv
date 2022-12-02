namespace AhjoApiService.AhjoApi.DTOs
{
    internal class AhjoVotingResultDTO
    {
        public AhjoVoterListDTO? Ayes { get; set; }

        public AhjoVoterListDTO? Noes { get; set; }

        public AhjoVoterListDTO? Blank { get; set; }

        public AhjoVoterListDTO? Absent { get; set; }
    }
}

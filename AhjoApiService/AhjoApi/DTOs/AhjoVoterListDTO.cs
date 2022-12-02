namespace AhjoApiService.AhjoApi.DTOs
{
    internal class AhjoVoterListDTO
    {
        public int? NumberOfVotes { get; set; }

        public AhjoVoterDTO[]? Voters { get; set; }

        public string? Content { get; set; }
    }
}

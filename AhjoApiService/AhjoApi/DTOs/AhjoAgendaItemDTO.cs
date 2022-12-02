namespace AhjoApiService.AhjoApi.DTOs
{
    internal class AhjoAgendaItemDTO
    {
        public int AgendaPoint { get; set; }

        public string? Section { get; set; }

        public string? AgendaItem { get; set; }

        public string? CaseIDLabel { get; set; }

        public string? Html { get; set; }

        public string? DecisionHistoryHTML { get; set; }
    }
}

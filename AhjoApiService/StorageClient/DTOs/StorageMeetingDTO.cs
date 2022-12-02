namespace AhjoApiService.StorageClient.DTOs
{
    internal class StorageMeetingDTO
    {
        public DateTime? MeetingDate { get; set; }

        public string? MeetingID { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }

        public List<StorageAgendaItemDTO>? Agendas { get; set; }

        public List<StorageDecisionDTO>? Decisions { get; set; }
    }
}

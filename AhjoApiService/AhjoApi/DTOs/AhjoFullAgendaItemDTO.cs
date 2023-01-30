using Newtonsoft.Json;

namespace AhjoApiService.AhjoApi.DTOs
{
    internal class AhjoFullAgendaItemDTO
    {
        [JsonProperty("agenda_item")]
        public AhjoAgendaItemDTO AgendaItem { get; set; }
    }
}

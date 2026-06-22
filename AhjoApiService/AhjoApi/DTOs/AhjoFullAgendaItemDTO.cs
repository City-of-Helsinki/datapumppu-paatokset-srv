using Newtonsoft.Json;

namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Wrapper for <see cref="AhjoAgendaItemDTO"/> when deserialised from the Ahjo agenda-item endpoint.
    /// The JSON property <c>agenda_item</c> is mapped via <see cref="JsonPropertyAttribute"/>.
    /// </summary>
    internal class AhjoFullAgendaItemDTO
    {
        /// <summary>The full agenda item details. Mapped from JSON property <c>agenda_item</c>.</summary>
        [JsonProperty("agenda_item")]
        public AhjoAgendaItemDTO AgendaItem { get; set; }
    }
}

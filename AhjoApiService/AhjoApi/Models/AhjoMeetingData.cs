using AhjoApiService.AhjoApi.DTOs;

namespace AhjoApiService.AhjoApi.Models
{
    internal class AhjoMeetingData
    {
        public AhjoMeetingDTO Meeting { get; set; }

        public AhjoAgendaItemDTO[] Agendas { get; set; }

        public AhjoFullDecisionDTO[] Decisions { get; set; }

        public AhjoMeetingData(AhjoMeetingDTO meeting, AhjoAgendaItemDTO[]? agendas = null, AhjoFullDecisionDTO[]? decisions = null)
        {
            Meeting = meeting;
            Agendas = agendas ?? Array.Empty<AhjoAgendaItemDTO>();
            Decisions = decisions ?? Array.Empty<AhjoFullDecisionDTO>();
        }
    }
}

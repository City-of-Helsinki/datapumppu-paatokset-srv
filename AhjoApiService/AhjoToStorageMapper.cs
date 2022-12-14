using AhjoApiService.AhjoApi.DTOs;
using AhjoApiService.AhjoApi.Models;
using AhjoApiService.StorageClient.DTOs;
using AutoMapper;

namespace AhjoApiService
{
    internal class AhjoToStorageMapper
    {
        public static List<StorageMeetingDTO> CreateStorageMeetingDTOs(List<AhjoMeetingData> ahjoMeetings)
        {
            var result = new List<StorageMeetingDTO>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AhjoAgendaItemDTO, StorageAgendaItemDTO>()
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.AgendaItem));
                cfg.CreateMap<AhjoFullMeetingDTO, StorageMeetingDTO>()
                    .ForMember(dest => dest.MeetingDate, opt => opt.MapFrom(src => src.DateMeeting))
                    .ForMember(dest => dest.Agendas, opt => opt.MapFrom(src => src.Agenda));
            });
            var mapper = config.CreateMapper();

            foreach (var ahjoMeetingData in ahjoMeetings)
            {
                var storageDecisions = ahjoMeetingData.Decisions?.Select(decisionData => MapToStorageDecision(decisionData)).ToList();
                var storageMeeting = mapper.Map<StorageMeetingDTO>(ahjoMeetingData.FullMeeting);

                storageMeeting.Decisions = storageDecisions;
                result.Add(storageMeeting);
            }
            return result;
        }

        private static StorageDecisionDTO MapToStorageDecision(AhjoDecisionData decisionData)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AhjoFullDecisionDTO, StorageDecisionDTO>()
                    .ForMember(dest => dest.MeetingID, opt => opt.MapFrom(_ => decisionData.MeetingID))
                    .ForMember(dest => dest.Html, opt => opt.MapFrom(src => src.Content));
                cfg.CreateMap<AhjoDecisionAttachmentDTO, StorageDecisionAttachmentDTO>();
            });
            var mapper = config.CreateMapper();

            return mapper.Map<StorageDecisionDTO>(decisionData.Decision);
        }

    }
}

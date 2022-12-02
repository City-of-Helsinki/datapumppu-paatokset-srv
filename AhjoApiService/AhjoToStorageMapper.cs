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
                cfg.CreateMap<AhjoDecisionAttachmentDTO, StorageDecisionAttachmentDTO>();
                cfg.CreateMap<AhjoFullDecisionDTO, StorageDecisionDTO>()
                    .ForMember(dest => dest.Html, opt => opt.MapFrom(src => src.Content));
                cfg.CreateMap<AhjoAgendaItemDTO, StorageAgendaItemDTO>()
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(origin => origin.AgendaItem));
                cfg.CreateMap<AhjoMeetingDTO, StorageMeetingDTO>()
                    .ForSourceMember(x => x.MeetingID, x => x.DoNotValidate())
                    .ForMember(dest => dest.MeetingDate, opt => opt.MapFrom(origin => origin.DateMeeting));
            });
            var mapper = config.CreateMapper();

            foreach (var ahjoMeetingData in ahjoMeetings)
            {
                var storageAgendas = ahjoMeetingData.Agendas?.Select(agenda => mapper.Map<StorageAgendaItemDTO>(agenda)).ToList();
                var storageDecisions = ahjoMeetingData.Decisions
                    ?.Select(decision => mapper.Map<StorageDecisionDTO>(decision)).ToList();
                var storageMeeting = mapper.Map<StorageMeetingDTO>(ahjoMeetingData.Meeting);

                storageMeeting.Agendas = storageAgendas;
                storageMeeting.Decisions = storageDecisions;
                result.Add(storageMeeting);
            }
            return result;
        }

    }
}

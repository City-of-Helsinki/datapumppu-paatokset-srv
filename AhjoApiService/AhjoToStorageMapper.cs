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
                cfg.CreateMap<AhjoAttachmentDTO, StorageAttachmentDTO>();
                cfg.CreateMap<AhjoFullDecisionDTO, StorageDecisionDTO>()
                    .ForMember(dest => dest.Language, opt => opt.MapFrom(src => GetLanguageFromHtml(string.IsNullOrEmpty(src.Content) ? src.Motion : src.Content)))
                    .ForMember(dest => dest.Html, opt => opt.MapFrom(src => src.Content));
                cfg.CreateMap<AhjoAgendaItemDTO, StorageAgendaItemDTO>()
                    .ForMember(dest => dest.Language, opt => opt.MapFrom(src => GetLanguageFromHtml(src.Html)))
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.AgendaItem));
                cfg.CreateMap<AhjoFullMeetingDTO, StorageMeetingDTO>()
                    .ForMember(dest => dest.MeetingDate, opt => opt.MapFrom(src => src.DateMeeting))
                    .ForMember(dest => dest.Agendas, opt => opt.MapFrom(src => src.Agenda));
            });
            var mapper = config.CreateMapper();

            foreach (var ahjoMeetingData in ahjoMeetings)
            {
                var storageDecisions = ahjoMeetingData.Decisions
                    ?.Select(decision => mapper.Map<StorageDecisionDTO>(decision)).ToList();
                var storageMeeting = mapper.Map<StorageMeetingDTO>(ahjoMeetingData.FullMeeting);

                storageMeeting.Decisions = storageDecisions;
                result.Add(storageMeeting);
            }
            return result;
        }

        private static string GetLanguageFromHtml(string html)
        {
            const int LangInfoStringLength = 20;
            if (html.Length >= LangInfoStringLength)
            {
                var substr = html.Substring(0, LangInfoStringLength);
                substr = substr.ToLower();
                if (substr.Contains("lang=\"sv\"")) return "sv";
                if (substr.Contains("lang=\"en\"")) return "en";
            }
            return "fi";
        }

    }
}

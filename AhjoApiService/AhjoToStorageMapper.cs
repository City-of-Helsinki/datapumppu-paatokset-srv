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
                cfg.CreateMap<AhjoAttachmentDTO, StorageAttachmentDTO>()
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => TruncateAttachmentTitle(src)));
                cfg.CreateMap<AhjoFullDecisionDTO, StorageDecisionDTO>()
                    .ForMember(dest => dest.Language, opt => opt.MapFrom(src => GetLanguageFromPdf(src.Pdf)))
                    .ForMember(dest => dest.Html, opt => opt.MapFrom(src => src.Content));
                cfg.CreateMap<AhjoAgendaItemDTO, StorageAgendaItemDTO>()
                    .ForMember(dest => dest.Language, opt => opt.MapFrom(src => GetLanguageFromPdf(src.Pdf)))
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

        private static string? TruncateAttachmentTitle(AhjoAttachmentDTO attachment)
        {
            const int MAX_DB_TITLE_LENGTH = 256;
            if (attachment?.Title == null)
                return null;
            return attachment.Title.Length > MAX_DB_TITLE_LENGTH
                ? attachment.Title.Substring(0, MAX_DB_TITLE_LENGTH)
                : attachment.Title;
        }

        private static string GetLanguageFromPdf(AhjoAttachmentDTO pdf)
        {
            if (pdf != null)
            {
                return pdf.Language;
            }
            return null;
        }

    }
}

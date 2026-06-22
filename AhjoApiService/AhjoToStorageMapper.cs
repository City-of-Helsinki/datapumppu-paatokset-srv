using AhjoApiService.AhjoApi.DTOs;
using AhjoApiService.AhjoApi.Models;
using AhjoApiService.StorageClient.DTOs;
using AutoMapper;

namespace AhjoApiService
{
    /// <summary>
    /// Transforms Ahjo API data models into Datapumppu Storage DTOs using AutoMapper.
    /// Handles custom mapping rules such as attachment title truncation and language extraction from PDF metadata.
    /// </summary>
    internal class AhjoToStorageMapper
    {
        /// <summary>
        /// Converts a list of <see cref="AhjoMeetingData"/> (meeting + decisions) into
        /// <see cref="StorageMeetingDTO"/> objects ready for posting to the Storage API.
        /// <para>
        /// Mapping rules:
        /// <list type="bullet">
        ///   <item><description><see cref="AhjoAttachmentDTO.Title"/> is truncated to 256 characters.</description></item>
        ///   <item><description><see cref="StorageDecisionDTO.Language"/> and <see cref="StorageAgendaItemDTO.Language"/> are extracted from the PDF attachment's language field.</description></item>
        ///   <item><description><see cref="AhjoFullDecisionDTO.Content"/> maps to <see cref="StorageDecisionDTO.Html"/>.</description></item>
        ///   <item><description><see cref="AhjoAgendaItemDTO.AgendaItem"/> maps to <see cref="StorageAgendaItemDTO.Title"/>.</description></item>
        ///   <item><description><see cref="AhjoFullMeetingDTO.DateMeeting"/> maps to <see cref="StorageMeetingDTO.MeetingDate"/>.</description></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="ahjoMeetings">The Ahjo meeting data to transform.</param>
        /// <returns>A list of storage-ready meeting DTOs.</returns>
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

        /// <summary>
        /// Truncates the attachment title to a maximum of 256 characters to satisfy database column constraints.
        /// </summary>
        /// <param name="attachment">The attachment whose title may be truncated.</param>
        /// <returns>The truncated title, or <c>null</c> if the attachment or its title is <c>null</c>.</returns>
        private static string? TruncateAttachmentTitle(AhjoAttachmentDTO attachment)
        {
            const int MAX_DB_TITLE_LENGTH = 256;
            if (attachment?.Title == null)
                return null;
            return attachment.Title.Length > MAX_DB_TITLE_LENGTH
                ? attachment.Title.Substring(0, MAX_DB_TITLE_LENGTH)
                : attachment.Title;
        }

        /// <summary>
        /// Extracts the language code from the PDF attachment metadata.
        /// </summary>
        /// <param name="pdf">The PDF attachment, or <c>null</c> if no PDF is available.</param>
        /// <returns>The language code (e.g. "fi", "sv"), or <c>null</c> if <paramref name="pdf"/> is <c>null</c>.</returns>
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

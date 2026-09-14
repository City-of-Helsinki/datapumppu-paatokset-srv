using AhjoApiService.AhjoApi.DTOs;

namespace AhjoApiService.AhjoApi.Models
{
    /// <summary>
    /// Internal container that pairs the full meeting details with the associated decisions.
    /// Produced by <see cref="AhjoApiReader"/> and consumed by <see cref="AhjoToStorageMapper"/>.
    /// </summary>
    internal class AhjoMeetingData
    {
        /// <summary>
        /// The full meeting details including agenda, composition, documents, and metadata.
        /// </summary>
        public AhjoFullMeetingDTO FullMeeting { get; set; }

        /// <summary>
        /// The approved decisions associated with the meeting. Empty when minutes have not been published.
        /// </summary>
        public AhjoFullDecisionDTO[] Decisions { get; set; }

        /// <summary>
        /// Initialises a new instance of <see cref="AhjoMeetingData"/>.
        /// </summary>
        /// <param name="fullMeeting">The full meeting details.</param>
        /// <param name="decisions">The decisions for the meeting, or <c>null</c> (defaults to an empty array).</param>
        public AhjoMeetingData(AhjoFullMeetingDTO fullMeeting, AhjoFullDecisionDTO[]? decisions = null)
        {
            FullMeeting = fullMeeting;
            Decisions = decisions ?? Array.Empty<AhjoFullDecisionDTO>();
        }
    }
}

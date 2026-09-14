using AhjoApiService.AhjoApi.DTOs;

namespace AhjoApiService.AhjoApi
{
    /// <summary>
    /// Defines operations for fetching meeting, decision, and agenda data from the Ahjo API.
    /// </summary>
    internal interface IAhjoApiClient
    {
        /// <summary>
        /// Fetches a list of meetings within the specified date range from the Ahjo API.
        /// </summary>
        /// <param name="startDate">Start of the date range (inclusive).</param>
        /// <param name="endDate">End of the date range (inclusive).</param>
        /// <returns>An array of meeting summaries, or <c>null</c> if the request fails.</returns>
        Task<AhjoMeetingDTO[]?> GetMeetings(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Fetches the full details of a single meeting from the Ahjo API.
        /// </summary>
        /// <param name="meetingDTO">The meeting summary containing the meeting ID to look up.</param>
        /// <returns>The full meeting details, or <c>null</c> if the request fails.</returns>
        Task<AhjoFullMeetingDTO?> GetMeetingDetails(AhjoMeetingDTO meetingDTO);

        /// <summary>
        /// Fetches all approved decisions for a given meeting from the Ahjo API.
        /// Each decision summary is enriched with full details via a secondary request.
        /// </summary>
        /// <param name="meetingID">The Ahjo meeting identifier.</param>
        /// <returns>An array of full decision DTOs, or an empty array if none are found or the request fails.</returns>
        Task<AhjoFullDecisionDTO[]> GetDecisions(string meetingID);

        /// <summary>
        /// Fetches full agenda item details for every agenda item in the given meeting.
        /// </summary>
        /// <param name="meetingDTO">The full meeting DTO whose <see cref="AhjoFullMeetingDTO.Agenda"/> items will be expanded.</param>
        /// <returns>An array of fully populated agenda items, or an empty array if none are found or the request fails.</returns>
        Task<AhjoAgendaItemDTO[]> GetFullAgenda(AhjoFullMeetingDTO meetingDTO);
    }
}

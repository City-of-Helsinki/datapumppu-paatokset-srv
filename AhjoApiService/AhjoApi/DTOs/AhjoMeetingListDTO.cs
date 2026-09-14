namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Wrapper for an array of <see cref="AhjoMeetingDTO"/> returned by the Ahjo meetings list endpoint.
    /// </summary>
    internal class AhjoMeetingListDTO
    {
        /// <summary>The array of meeting summaries.</summary>
        public AhjoMeetingDTO[]? Meetings { get; set; }
    }
}

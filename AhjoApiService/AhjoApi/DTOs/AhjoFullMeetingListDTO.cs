using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Wrapper for an array of <see cref="AhjoFullMeetingDTO"/> returned by the Ahjo single-meeting endpoint.
    /// </summary>
    internal class AhjoFullMeetingListDTO
    {
        /// <summary>The array of full meeting details.</summary>
        public AhjoFullMeetingDTO[]? Meetings { get; set; }
    }
}

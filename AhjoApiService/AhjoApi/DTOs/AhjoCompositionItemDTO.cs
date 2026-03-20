using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhjoApiService.AhjoApi.DTOs
{
    /// <summary>
    /// Represents a member of the meeting body composition (attendees, chairperson, deputies).
    /// </summary>
    internal class AhjoCompositionItemDTO
    {
        /// <summary>Full name of the member.</summary>
        public string? Name { get; set; }

        /// <summary>Role of the member in the meeting (e.g. "chairperson", "member", "secretary").</summary>
        public string? Role { get; set; }

        /// <summary>Name of the member this person is deputising for. <c>null</c> if not a deputy.</summary>
        public string? DeputyOf { get; set; }
    }
}

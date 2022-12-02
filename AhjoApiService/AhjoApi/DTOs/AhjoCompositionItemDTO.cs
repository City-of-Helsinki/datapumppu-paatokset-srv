using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhjoApiService.AhjoApi.DTOs
{
    internal class AhjoCompositionItemDTO
    {
        public string? Name { get; set; }

        public string? Role { get; set; }

        public string? DeputyOf { get; set; }
    }
}

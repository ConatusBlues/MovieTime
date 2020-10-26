using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTime.Models.DTO
{
    public class SummaryDTO
    {
        public Global Global { get; set; }
        public List<SummaryDetailDto> Movies { get; set; }
    }
}

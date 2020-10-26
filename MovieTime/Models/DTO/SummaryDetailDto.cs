using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTime.Models.DTO
{
    public class SummaryDetailDto
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
    }
}

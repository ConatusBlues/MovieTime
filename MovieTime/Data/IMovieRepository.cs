using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTime.Data
{
    public class IMovieRepository
    {
        Task<IEnumerable<MovieDto>> GetMovies();
        Task<SummaryDTO> GetSummary();
        Task<SummaryViewModel> GetSummaryViewModel(string movie = null);
    }
}

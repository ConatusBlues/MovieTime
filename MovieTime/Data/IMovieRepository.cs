using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTime.Models.ViewModels;
using MovieTime.Models.DTO;
using MovieTime.DTO;

namespace MovieTime.Data
{
    public interface IMovieRepository
    {
        Task<IEnumerable<MovieDto>> GetMovies();
        Task<SummaryDTO> GetSummary();
        Task<SummaryViewModel> GetSummaryViewModel(string movie = null);
    }
}

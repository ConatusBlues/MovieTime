using Microsoft.Extensions.Configuration;
using MovieTime.DTO;
using MovieTime.Infrastructure;
using MovieTime.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTime.Data
{
    public class MovieRepository : IMovieRepository
    {
        
        private string baseUrl, defaultMovie;
        IApiClient apiClient;
        public MovieRepository(IConfiguration configuration, IApiClient apiClient)
        {
            baseUrl = configuration.GetValue<string>("MovieApi:BaseUrl");
            defaultMovie = configuration.GetValue<string>("MovieApi:DefaultMovie");
            this.apiClient = apiClient;
        }
        public async Task<IEnumerable<MovieDto>> GetMovies()
        {
            return await apiClient.GetAsync<IEnumerable<MovieDto>>(baseUrl + "movies");
        }

        public async Task<SummaryDTO> GetSummary()
        {
            return await apiClient.GetAsync<SummaryDTO>(baseUrl + "summary");
        }

        public async Task<SummaryViewModel> GetSummaryViewModel(string movie = null)
        {
            movie = movie ?? defaultMovie;
            var tasks = new List<Task>(); // en lista med olika trådar

            var movies = apiClient.GetAsync<IEnumerable<MovieDto>>(baseUrl + "movies"); // ett nytt uppdrag
            var summary = apiClient.GetAsync<SummaryDTO>(baseUrl + "summary"); // ännu ett uppdrag

            tasks.Add(movies); // koppla ihop uppdraget med trådarna
            tasks.Add(summary);
            await Task.WhenAll(tasks); // kör alla trådar, parallellt

            SummaryDetailDto summaryDetail = summary.Result.Movies
                .Where(c => c.Movie.Equals(movie))
                .FirstOrDefault();
            return new SummaryViewModel(movies.Result, summaryDetail);
            
        }

    }
}

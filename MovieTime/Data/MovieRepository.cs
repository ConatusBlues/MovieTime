using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MovieTime.Infrastructure;
using MovieTime.Models.DTO;
using MovieTime.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

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
                .Where(c => c.Title.Equals(movie))
                .FirstOrDefault();
            return new SummaryViewModel(movies.Result, summaryDetail);
            
        }

    }
}

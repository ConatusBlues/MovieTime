using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using MovieTime.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieTime.Models.ViewModels
{
    public class SummaryViewModel
    {
        [Display(Name = "Nya bekräftade fall")]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString = "{0:N0} st")]
        public int NewConfirmed { get; set; }

        [Display(Name = "Totala antalet bekräftade fall")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N0}")]
        public int TotalConfirmed { get; set; }
        public int TotalDeaths { get; set; }
        [DisplayFormat(DataFormatString = "{0:dddd dd MMMM}")]
        public DateTime Date { get; set; }

        private List<Movie> movies;

        public string SelectedMovie { get; set; } = "Sweden";
        
        [Display(Name ="Välj film")]
        public IEnumerable<SelectListItem> Movies
        {
            get
            {
                if (movies != null)
                {
                    return movies.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Name
                    });
                }
                return null;
            }
        }

        public SummaryViewModel(IEnumerable<MovieDto> movies, SummaryDetailDto summaryDetail)
        {
            // ger alla värden till våra properties
            if (summaryDetail != null)
            {
                NewConfirmed = summaryDetail.NewConfirmed;
                TotalConfirmed = summaryDetail.TotalConfirmed;
                TotalDeaths = summaryDetail.TotalDeaths;
                Date = summaryDetail.Date;
            }
            

            // gör om countryDto till en lista av countries
           this.movies = movies
                .Select(c => new Movie
                {
                    Name = c.Movies
                })
                .OrderBy(x => x.Name)
                .ToList();
        }

        public SummaryViewModel()
        {

        }
    }
}

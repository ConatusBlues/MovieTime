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
        [Display(Name = "Title")]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString = "{0:N0} st")]
        public string Title { get; set; }

        [Display(Name = "Director")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N0}")]
        public string Director { get; set; }
        public string Genre { get; set; }
        [DisplayFormat(DataFormatString = "{0:dddd dd MMMM}")]

        private List<Movie> movies;

        
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
                Title = summaryDetail.Title;
                Director = summaryDetail.Director;
                Genre = summaryDetail.Genre;
            }
            

            // gör om countryDto till en lista av countries
           this.movies = movies
                .Select(c => new Movie
                {
                    Name = c.Movie
                })
                .OrderBy(x => x.Name)
                .ToList();
        }

        public SummaryViewModel()
        {

        }
    }
}

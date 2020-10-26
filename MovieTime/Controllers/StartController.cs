using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTime.Data;
using MovieTime.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MovieTime.Controllers
{
    public class StartController : Controller
    {
        private IMovieRepository movieRepository;

        public StartController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await movieRepository.GetMovies();
                return View(model);

            }
            catch (Exception)
            {

                return RedirectToAction("index", "error");
            }
        }

    }
}

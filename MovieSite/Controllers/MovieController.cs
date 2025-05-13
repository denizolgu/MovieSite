using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieSite.Extensions;
using MovieSite.Models;

namespace MovieSite.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Info(int? id)
        {
            if (id == null)
            {
                ViewBag.Message = "Please specify a movie ID!!!";
                return View("Error");
            }

            var movies = HttpContext.Session.Get<List<Movie>>("Movies");
            if (movies == null || !movies.Any())
            {
                ViewBag.Message = "Invalid movie ID!!!";
                return View("Error");
            }

            var movie = movies.FirstOrDefault(m => m.MovieID == id);
            if (movie == null)
            {
                ViewBag.Message = "Invalid movie ID!!!";
                return View("Error");
            }

            var cart = HttpContext.Session.Get<List<int>>("Cart") ?? new List<int>();
            ViewBag.IsInCart = cart.Contains(movie.MovieID);

            return View(movie);
        }
    }
}

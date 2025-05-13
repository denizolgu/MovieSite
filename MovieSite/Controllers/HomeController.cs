using Microsoft.AspNetCore.Mvc;
using MovieSite.Extensions;
using MovieSite.Models;
using System.Diagnostics;

namespace MovieSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (Request.Cookies["UserInfo"] == null)
            {
                ViewBag.IsLoggedIn = false;
            }
            else
            {
                ViewBag.IsLoggedIn = true;
                ViewBag.UserName = Request.Cookies["UserInfo"];
            }

            var movies = HttpContext.Session.Get<List<Movie>>("Movies");

            if (movies == null)
            {
                movies = new List<Movie>
            {
                new Movie { MovieID = 1, Title = "X-Men: The Last Stand", Director = "Brett Ratner", Stars = new[] { "Patrick Stewart", "Hugh Jackman", "Halle Berry" }, ReleaseYear = 2006, ImageUrl = "xmen.jpg" },
                new Movie { MovieID = 2, Title = "Spider Man 2", Director = "Sam Raimi", Stars = new[] { "Tobey Maguire", "Kirsten Dunst", "Alfred Molina" }, ReleaseYear = 2004, ImageUrl = "spiderman2.jpg" },
                new Movie { MovieID = 3, Title = "Spider Man 3", Director = "Sam Raimi", Stars = new[] { "Tobey Maguire", "Kirsten Dunst", "Topher Grace" }, ReleaseYear = 2007, ImageUrl = "spiderman3.jpg" },
                new Movie { MovieID = 4, Title = "Valkyrie", Director = "Bryan Singer", Stars = new[] { "Tom Cruise", "Bill Nighy", "Carice van Houten" }, ReleaseYear = 2008, ImageUrl = "valkyrie.jpg" },
                new Movie { MovieID = 5, Title = "Gladiator", Director = "Ridley Scott", Stars = new[] { "Russell Crowe", "Joaquin Phoenix", "Connie Nielsen" }, ReleaseYear = 2000, ImageUrl = "gladiator.jpg" }
            };
                HttpContext.Session.Set("Movies", movies);
            }

            ViewBag.Movies = movies;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

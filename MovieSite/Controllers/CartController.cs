using Microsoft.AspNetCore.Mvc;
using MovieSite.Extensions;
using MovieSite.Models;

namespace MovieSite.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var x = 5;
            var cart = HttpContext.Session.Get<List<int>>("Cart") ?? new List<int>();
            var movies = HttpContext.Session.Get<List<Movie>>("Movies") ?? new List<Movie>();

            var cartMovies = movies.Where(m => cart.Contains(m.MovieID)).ToList();
            return View(cartMovies);
        }

        public IActionResult AddToCart(int id)
        {
            var cart = HttpContext.Session.Get<List<int>>("Cart") ?? new List<int>();

            if (cart.Contains(id))
            {
                TempData["CartMessage"] = "Movie is already in the list!";
            }
            else
            {
                cart.Add(id);
                HttpContext.Session.Set("Cart", cart);
                TempData["CartMessage"] = "Movie is added to the cart!";
            }

            return RedirectToAction("Info", "Movie", new { id });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.Get<List<int>>("Cart") ?? new List<int>();

            if (cart.Contains(id))
            {
                cart.Remove(id);
                HttpContext.Session.Set("Cart", cart);
            }

            return RedirectToAction("Index");
        }
    }
}

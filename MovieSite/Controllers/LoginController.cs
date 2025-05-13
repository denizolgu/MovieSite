using Microsoft.AspNetCore.Mvc;

namespace MovieSite.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string firstName, string lastName)
        {
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddMonths(1)
                };
                Response.Cookies.Append("UserInfo", firstName + " " + lastName, cookieOptions);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("UserInfo");
            return RedirectToAction("Index", "Home");
        }
    }
}

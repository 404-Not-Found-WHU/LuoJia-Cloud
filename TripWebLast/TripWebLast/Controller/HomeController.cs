using Microsoft.AspNetCore.Mvc;

namespace TripWebLast.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
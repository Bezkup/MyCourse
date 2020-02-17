using Microsoft.AspNetCore.Mvc;

namespace src
{
    public class HomeController : Controller
    {
        [ResponseCache(CacheProfileName = "Home")]
        public IActionResult Index(){
            ViewData["Title"] = "Benvenuto su MyCourse";
            return View();
        }
    }
}
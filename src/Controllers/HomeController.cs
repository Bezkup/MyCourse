using Microsoft.AspNetCore.Mvc;

namespace src
{
    public class HomeController : Controller
    {
        public IActionResult Index(){
            return View();
        }
    }
}
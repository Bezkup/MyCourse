using Microsoft.AspNetCore.Mvc;

namespace MyCourse
{
    public class HomeController : Controller
    {
        public IActionResult Index(){
            return View();
        }
    }
}
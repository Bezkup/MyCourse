using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Models.ViewModels;
using src.Models.Services.Application;

namespace src.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICachedCourseService courseService;

        public CoursesController(ICachedCourseService courseService)
        {
            this.courseService = courseService;
        }
        public async Task<IActionResult> Index(){
            ViewData["Title"] = "Catalogo dei corsi";

            List<CourseViewModel> lista = await courseService.GetCoursesAsync();
            return View(lista);
        } 

        public async Task<IActionResult> Detail(int id){
            
            CourseDetailViewModel viewModel = await courseService.GetCourseAsync(id);
            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }
    }
}
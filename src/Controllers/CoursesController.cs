using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using src.Model.Services.Application;
using src.Models.ViewModels;

namespace src.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index(){
            var courseService = new CourseService();
            List<CourseViewModel> lista = courseService.GetServices();
            return View(lista);
        } 

        public IActionResult Detail(string id){
    
            return View();
        }
    }
}
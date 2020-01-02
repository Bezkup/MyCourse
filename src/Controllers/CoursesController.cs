using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.ViewModels;
using src.Models.Services.Application;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        public IActionResult Index(){
            ViewData["Title"] = "Catalogo dei corsi";

            List<CourseViewModel> lista = courseService.GetCourses();
            return View(lista);
        } 

        public IActionResult Detail(int id){
            
            CourseDetailViewModel viewModel = courseService.GetCourse(id);
            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }
    }
}
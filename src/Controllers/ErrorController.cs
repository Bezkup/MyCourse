using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using src.Models.Services.Application;
using src.Models.ViewModels;

namespace src.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IErrorViewSelectorService errorViewSelector;
        public ErrorController(IErrorViewSelectorService errorViewSelector)
        {
            this.errorViewSelector = errorViewSelector;
        }
        public IActionResult Info()
        {      
         
            ErrorViewModel result = errorViewSelector.ScanError(HttpContext.Features.Get<IExceptionHandlerPathFeature>());
            ViewData["Title"] = result.Title;
            Response.StatusCode = result.StatusCode;

            return View(result.View);
        }
    }
}
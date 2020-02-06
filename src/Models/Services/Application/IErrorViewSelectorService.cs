using Microsoft.AspNetCore.Diagnostics;
using src.Models.ViewModels;

namespace src.Models.Services.Application
{
    public interface IErrorViewSelectorService
    {
         ErrorViewModel ScanError(IExceptionHandlerPathFeature ex);
    }

}
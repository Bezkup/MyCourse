using Microsoft.AspNetCore.Diagnostics;
using src.Models.Exceptions;
using src.Models.ViewModels;

namespace src.Models.Services.Application
{
    public class ErrorViewSelectorService : IErrorViewSelectorService
    {
        public ErrorViewModel ScanError(IExceptionHandlerPathFeature ex)
        {
            switch(ex.Error){

                case CourseNotFoundException exc:
                    return new ErrorViewModel{
                        Title = "Dati Mancanti",
                        StatusCode = 404,
                        View = "DetailNotFound"
                    };

                default:
                    return new ErrorViewModel{
                        Title = "Errore",
                        StatusCode = 500,
                        View = ""
                    };
            }
        }
    }
}
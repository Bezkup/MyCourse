using System.Collections.Generic;
using System.Threading.Tasks;
using src.Models.ViewModels;

namespace src.Models.Services.Application
{
    public interface ICourseService
    {
        Task<List<CourseViewModel>> GetCoursesAsync();
        Task<CourseDetailViewModel> GetCourseAsync(int id);    
    }
}
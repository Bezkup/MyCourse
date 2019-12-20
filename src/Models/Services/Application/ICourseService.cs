using System.Collections.Generic;
using MyCourse.Models.ViewModels;

namespace src.Models.Services.Application
{
    public interface ICourseService
    {
        List<CourseViewModel> GetCourses();
        CourseDetailViewModel GetCourse(int id);    
    }
}
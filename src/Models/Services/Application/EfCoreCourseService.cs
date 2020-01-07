using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsoDotNet.Models.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MyCourse.Models.ViewModel;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application
{
    public class EfCoreCourseService : ICourseService
    {
        private readonly MyCourseDbContext dbContext;

        public EfCoreCourseService(MyCourseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
           IQueryable<CourseDetailViewModel> queryLinq = dbContext.Courses
                    .Include(course => course.Lessons)
                    .Where(course => course.Id == id)
                    .Select(course => CourseDetailViewModel.FromEntity(course)).AsNoTracking();
                    //.FirstOrDefaultAsync() //Restituisce null se l'elenco è vuoto ma non solleva eccezione
                    //.SingleOrDefaultAsync(); // tollera il fatto che l'elenco sia vuoto e in quel caso restituisce null, oppure se l'elenco contiene più di un elemento solleva eccezione
                    //.FirstAsync(); //Restitutisce il primo elemento ma se l'elenco è vuoto lancia eccezione
                    // .SingleAsync(); //Restituisce il primo elemento ma se ne trova più di uno lancia eccezione

                    CourseDetailViewModel viewModel = await queryLinq.SingleAsync();

                    return viewModel;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            IQueryable<CourseViewModel> queryLinq = dbContext.Courses
            .Select(
                course => CourseViewModel.FromEntity(course)
            );
            
            List<CourseViewModel> courses = await queryLinq
                                                        .AsNoTracking()
                                                        .ToListAsync();
            return courses;
        }
    }
}
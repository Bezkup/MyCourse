using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using src.Models.ViewModels;
using Microsoft.Extensions.Logging;
using src.Models.Exceptions;
using Microsoft.Extensions.Options;
using src.Models.Options;

namespace src.Models.Services.Application
{
    public class EfCoreCourseService : ICourseService
    {
        private readonly IOptionsMonitor<CoursesOptions> coursesOptions;
        private readonly ILogger<EfCoreCourseService> logger;
        private readonly MyCourseDbContext dbContext;

        public EfCoreCourseService(IOptionsMonitor<CoursesOptions> coursesOptions, ILogger<EfCoreCourseService> logger, MyCourseDbContext dbContext)
        {
            this.coursesOptions = coursesOptions;
            this.logger = logger;
            this.dbContext = dbContext;
        }
        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            IQueryable<CourseDetailViewModel> queryLinq = dbContext.Courses
                    .Include(course => course.Lessons)
                    .Where(course => course.Id == id)
                    .Select(course => CourseDetailViewService.FromEntity(course)).AsNoTracking();
                    //.FirstOrDefaultAsync() //Restituisce null se l'elenco è vuoto ma non solleva eccezione
                    //.SingleOrDefaultAsync(); // tollera il fatto che l'elenco sia vuoto e in quel caso restituisce null, oppure se l'elenco contiene più di un elemento solleva eccezione
                    //.FirstAsync(); //Restitutisce il primo elemento ma se l'elenco è vuoto lancia eccezione
                    // .SingleAsync(); //Restituisce il primo elemento ma se ne trova più di uno lancia eccezione

            CourseDetailViewModel viewModel = await queryLinq.SingleAsync();

            if(viewModel == null){
                logger.LogWarning("Course {id} not found", id);
                throw new CourseNotFoundException(id);
            }    
            return viewModel;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            IQueryable<CourseViewModel> queryLinq = dbContext.Courses
            .Select(
                course => CourseViewService.FromEntity(course)
            );
            
            List<CourseViewModel> courses = await queryLinq
                                                        .AsNoTracking()
                                                        .ToListAsync();
            return courses;
        }
    }
}
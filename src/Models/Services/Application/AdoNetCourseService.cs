using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using src.Models.ViewModels;
using src.Models.Services.Infrastructure;
using Microsoft.Extensions.Options;
using src.Models.Options;
using Microsoft.Extensions.Logging;
using src.Models.Exceptions;
using src.Models.ViewModel;

namespace src.Models.Services.Application
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly ILogger<AdoNetCourseService> logger;
        private readonly IDatabaseAccessor db;
        private readonly IOptionsMonitor<CoursesOptions> coursesOptions;
        public AdoNetCourseService(ILogger<AdoNetCourseService> logger, IDatabaseAccessor db, IOptionsMonitor<CoursesOptions> courseOptions){
            this.logger = logger;
            this.db = db;
            CourseOptions = courseOptions;
        }

        public IOptionsMonitor<CoursesOptions> CourseOptions { get; }

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
    {

            logger.LogInformation("Course {id} requested", id);

            FormattableString query = $@"Select Id, Title, Description, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses WHERE Id={id}; SELECT Id, Title, Description, Duration FROM Lessons WHERE CourseId={id}";

            DataSet dataSet = await db.QueryAsync(query);

            //Course
            var courseTable = dataSet.Tables[0];
            if(courseTable.Rows.Count != 1){
                logger.LogWarning("Course {id} not found", id);
                throw new CourseNotFoundException(id);
            }
            var courseRow = courseTable.Rows[0];
            var courseDetailViewModel = CourseDetailViewService.FromDataRow(courseRow);
            
            //Course Lessons
            var lessonDataTable = dataSet.Tables[1];
            
            foreach(DataRow lessonRow in lessonDataTable.Rows){
                LessonViewModel lessonViewModel = LessonViewService.FromDataRow(lessonRow);
                courseDetailViewModel.Lessons.Add(lessonViewModel);
            }
            
            return courseDetailViewModel;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            FormattableString query = $@"SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency  FROM Courses";
            DataSet dataset = await db.QueryAsync(query);
            var dataTable = dataset.Tables[0];
            var courseList = new List<CourseViewModel>();
            foreach(DataRow courseRow in dataTable.Rows){
                CourseViewModel course = CourseViewService.FromDataRow(courseRow);
                courseList.Add(course);
            }
            return courseList;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MyCourse.Models.ViewModel;
using MyCourse.Models.ViewModels;
using MyCourse.Models.Services.Infrastructure;
using Microsoft.Extensions.Options;
using src.Models.Options;
using Microsoft.Extensions.Logging;

namespace MyCourse.Models.Services.Application
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly ILogger<AdoNetCourseService> logger;
        private readonly IDatabaseAccessor db;

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
                throw new InvalidOperationException($"Did not return exactly 1 row from Course {id}");
            }
            var courseRow = courseTable.Rows[0];
            var courseDetailViewModel = CourseDetailViewModel.FromDataRow(courseRow);
            
            //Course Lessons
            var lessonDataTable = dataSet.Tables[1];
            foreach(DataRow lessonRow in lessonDataTable.Rows){
                LessonViewModel lessonViewModel = LessonViewModel.FromDataRow(lessonRow);
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
                CourseViewModel course = CourseViewModel.FromDataRow(courseRow);
                courseList.Add(course);
            }
            return courseList;
        }
    }
}
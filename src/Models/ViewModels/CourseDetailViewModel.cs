using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CorsoDotNet.Models.Entities;
using MyCourse.Models.Enums;
using MyCourse.Models.ValueTypes;
using MyCourse.Models.ViewModel;

namespace MyCourse.Models.ViewModels
{
    public class CourseDetailViewModel : CourseViewModel
    {
        public List<LessonViewModel> Lessons { get; set; }
        public string Description { get; set; }

        public TimeSpan TotalTime{
            get => TimeSpan.FromSeconds(Lessons?.Sum(l => l.Duration.TotalSeconds) ?? 0);
        }

        public static new CourseDetailViewModel FromDataRow(DataRow courseRow){
            var courseDetailViewModel = new CourseDetailViewModel{
                Title = Convert.ToString(courseRow["Title"]),
                Description = Convert.ToString(courseRow["Description"]),
                Author = Convert.ToString(courseRow["Author"]),
                ImagePath = Convert.ToString(courseRow["ImagePath"]),
                Rating = Convert.ToDouble(courseRow["Rating"]),
                FullPrice = new Money(
                    Enum.Parse<Currency>(Convert.ToString(courseRow["FullPrice_Currency"])),
                    Convert.ToDecimal(courseRow["FullPrice_Amount"])
                ),
                CurrentPrice = new Money(
                    Enum.Parse<Currency>(Convert.ToString(courseRow["CurrentPrice_Currency"])),
                    Convert.ToDecimal(courseRow["CurrentPrice_Amount"])
                ),
                Id = Convert.ToInt32(courseRow["Id"]),
                Lessons = new List<LessonViewModel>()
            };
            return courseDetailViewModel;
        }

        internal static new CourseDetailViewModel FromEntity(Course course)
        {
            CourseDetailViewModel viewModel = new CourseDetailViewModel{
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                ImagePath = course.ImagePath,
                Author = course.Author,
                Rating = course.Rating,
                CurrentPrice = course.CurrentPrice,
                FullPrice = course.FullPrice
            };
            return viewModel;
        }
    }
}
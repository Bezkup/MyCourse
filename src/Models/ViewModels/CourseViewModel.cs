using System;
using System.Data;
using CorsoDotNet.Models.Entities;
using MyCourse.Models.Enums;
using MyCourse.Models.ValueTypes;

namespace MyCourse.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id {get;set;}
        public string Title {get;set;}
        public string ImagePath {get;set;}
        public string Author{get;set;}
        public double Rating{get;set;}
        public Money FullPrice{get;set;}
        public Money CurrentPrice{get;set;}

        public static CourseViewModel FromDataRow(DataRow courseRow)
        {
            var courseViewModel = new CourseViewModel{
                Title = Convert.ToString(courseRow["Title"]),
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
                Id = Convert.ToInt32(courseRow["Id"])
            };
            return courseViewModel;
        }

        internal static CourseViewModel FromEntity(Course course)
        {
            CourseViewModel viewModel = new CourseViewModel{
                Id = course.Id,
                Title = course.Title,
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

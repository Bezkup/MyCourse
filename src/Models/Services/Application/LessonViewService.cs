using System;
using System.Data;
using src.Models.Entities;
using src.Models.ViewModel;

namespace src.Models.Services.Application
{
    public class LessonViewService
    {
         public static LessonViewModel FromDataRow(DataRow dataRow)
        {
            var lessonViewModel = new LessonViewModel {
                Id = Convert.ToInt32(dataRow["Id"]),
                Title = Convert.ToString(dataRow["Title"]),
                Description = Convert.ToString(dataRow["Description"]),
                Duration = TimeSpan.Parse(Convert.ToString(dataRow["Duration"])),
            };
            return lessonViewModel;
        }

        public static LessonViewModel FromEntity(Lesson lesson)
        {
            return new LessonViewModel
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Duration = lesson.Duration,
                Description = lesson.Description
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
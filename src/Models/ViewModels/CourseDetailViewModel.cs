using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using src.Models.Entities;
using src.Models.Enums;
using src.Models.ValueTypes;
using src.Models.ViewModel;

namespace src.Models.ViewModels
{
    public class CourseDetailViewModel : CourseViewModel
    {
        public CourseDetailViewModel()
        {
            Lessons = new List<LessonViewModel>();
        }

        public List<LessonViewModel> Lessons { get; set; }
        public string Description { get; set; }

        public TimeSpan TotalTime{
            get => TimeSpan.FromSeconds(Lessons?.Sum(l => l.Duration.TotalSeconds) ?? 0);
        }
    }
}
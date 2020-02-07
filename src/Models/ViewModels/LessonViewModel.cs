using System;
using System.Data;
using src.Models.Entities;

namespace src.Models.ViewModel
{
    public class LessonViewModel
    {
       public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }

    }
}
using System;
using System.Data;
using src.Models.Entities;
using src.Models.Enums;
using src.Models.ValueTypes;

namespace src.Models.ViewModels
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

        
    }
}

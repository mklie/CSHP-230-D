using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomePage.Models;
using HomePageEF;

namespace HomePage
{

    public interface ICourseRepository
    {
        IEnumerable<HomePage.Models.CourseModel> Courses { get; }
    }

    public class CourseRepository : ICourseRepository
    {
        HomePageDBEntities context = new HomePageDBEntities();
        public IEnumerable<HomePage.Models.CourseModel> Courses
        {
            get
            {
                return context.Classes;
            }
        }
    }
}
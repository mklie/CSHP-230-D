using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomePageEF;

namespace HomePage.Business
{
    public interface ICourseManager
    {
        CourseModel[] Courses { get; }
        CourseModel Course(int classId);
    }

    public class CourseModel

    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }


        public CourseModel(int classId, string className, string classDescription, decimal classPrice)
        {
            ClassId = classId;
            ClassName = className;
            ClassDescription = classDescription;
            ClassPrice = classPrice;

        }
    }


    public class CourseManager : ICourseManager
    {
        private readonly ICourseRepository courseRepository;

        public CourseManager(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public CourseModel[] Courses
        {
            get
            {
                return courseRepository.Courses
                                         .Select(t => new CourseModel (t.ClassId, t.ClassName, t.ClassDescription, t.ClassPrice))
                                         .ToArray();
            }
        }

        public CourseModel Course(int classId)
        {
            var courseModel = courseRepository.Course(classId);
            return new CourseModel (courseModel.ClassId, courseModel.ClassName, courseModel.ClassDescription, courseModel.ClassPrice);
        }
    }
}
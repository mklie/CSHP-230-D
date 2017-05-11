using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HomePageEF
{
    public interface ICourseRepository
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
    }

        public class CourseRepository : ICourseRepository
    {
        public CourseModel[] Courses
        {
            get
            {
                return DBAccessor.Instance.Classes
                                               .Select(t => new CourseModel { ClassId = t.ClassId, ClassName = t.ClassName, ClassDescription = t.ClassDescription, ClassPrice = t.ClassPrice })
                                               .ToArray();
            }
        }

        public CourseModel Course(int classId)
        {
            var course = DBAccessor.Instance.Classes
                                                   .Where(t => t.ClassId == classId)
                                                   .Select(t => new CourseModel { ClassId = t.ClassId, ClassName = t.ClassName })
                                                   .First();
            return course;
        }
    }
}

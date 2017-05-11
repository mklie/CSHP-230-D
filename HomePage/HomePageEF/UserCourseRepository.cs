using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomePageEF
{
    public interface IUserCourseRepository
    {
        UserCourseModel Add(int userId, int courseId);
        bool Remove(int userId, int courseId);
        UserCourseModel[] GetAll(int userId);
        
    }

    public class UserCourseModel
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }

    }

    public class UserCourseRepository : IUserCourseRepository
    {
        public UserCourseModel Add(int userId, int courseId)
        {
            var user = DBAccessor.Instance.Users.FirstOrDefault(
                u => u.UserId == userId);
            var course = DBAccessor.Instance.Classes.FirstOrDefault(
                c => c.ClassId == courseId);
            user.Classes.Add(course);


            DBAccessor.Instance.SaveChanges();

            return new UserCourseModel { UserId = user.UserId, ClassId = course.ClassId };
        }

        public UserCourseModel[] GetAll (int userId)
        {
            var courses = DBAccessor.Instance.Classes
                .Where(u => u.Users.Any(c => c.UserId == userId))
                .Select(u => new UserCourseModel { UserId = userId, ClassId = u.ClassId, ClassName = u.ClassName, ClassDescription = u.ClassDescription, ClassPrice = u.ClassPrice }).ToArray();
            return courses;
        }

        public bool Remove(int userId, int courseId)
        {
            var user = DBAccessor.Instance.Users.FirstOrDefault(
                u => u.UserId == userId);
            var course = DBAccessor.Instance.Classes.FirstOrDefault(
                c => c.ClassId == courseId);
            user.Classes.Remove(course);


            //DBAccessor.Instance.ShoppingCartItems.Remove(items.First());

            DBAccessor.Instance.SaveChanges();

            return true;
        }
    }
}

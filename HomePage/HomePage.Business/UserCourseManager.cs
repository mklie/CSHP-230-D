using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomePageEF;

namespace HomePage.Business
{
    public interface IUserCourseManager
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

    public class UserCourseManager : IUserCourseManager
    {
        private readonly IUserCourseRepository userCourseRepository;

        public UserCourseManager(IUserCourseRepository userCourseRepository)
        {
            this.userCourseRepository = userCourseRepository;
        }

        public UserCourseModel Add(int userId, int courseId)
        {
            var course = userCourseRepository.Add(userId, courseId);

            return new UserCourseModel { UserId = course.UserId, ClassId = course.ClassId };
        }

        public UserCourseModel[] GetAll(int userId)
        {
            var courses = userCourseRepository.GetAll(userId)
                .Select(t => new UserCourseModel { UserId = t.UserId, ClassId = t.ClassId, ClassName = t.ClassName, ClassDescription = t.ClassDescription, ClassPrice = t.ClassPrice })
                .ToArray();

            return courses;
        }

        public bool Remove(int userId, int courseId)
        {
            return userCourseRepository.Remove(userId, courseId);
        }
    }
}

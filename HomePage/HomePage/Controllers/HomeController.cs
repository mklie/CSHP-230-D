using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomePage.Models;
using System.Web.UI;
using HomePage.Business;

namespace HomePage.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ICourseManager courseManager;
        private readonly IUserManager userManager;
        private readonly IUserCourseManager userCourseManager;

        public HomeController (ICourseManager courseManager,
                      IUserManager userManager, IUserCourseManager userCourseManager)
        {
            this.courseManager = courseManager;
            this.userManager = userManager;
            this.userCourseManager = userCourseManager;
        }

        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Course(int id)
        {
           return View(courseManager.Courses.First());
        }
        
        public ActionResult Courses()
        {
            var courses = courseManager.Courses
                                        .Select(t => new Models.CourseModel(t.ClassId, t.ClassName, t.ClassDescription, t.ClassPrice))
                                        .ToArray();
            var model = new IndexModel { Courses = courses };
            return View(courses);
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.UserEmail, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    Session["User"] = new Models.UserModel { Id = user.Id, Email = user.Email};

                    System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.UserEmail, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            return Redirect("~/");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Register(registerModel.UserEmail, registerModel.Password);

                Session["User"] = new Models.UserModel { Id = user.Id, Email = user.Email };

                System.Web.Security.FormsAuthentication.SetAuthCookie(registerModel.UserEmail, false);

                return Redirect(returnUrl ?? "~/");

            }

            return View(registerModel);
        }

        [Authorize]
        public ActionResult EnrollInClass()
        {
            var user = (Models.UserModel)Session["User"];
            var courses = courseManager.Courses
                                        .Select(t => new Models.CourseModel(t.ClassId, t.ClassName, t.ClassDescription, t.ClassPrice))
                                        .ToArray();
            var model = new IndexModel { Courses = courses };
            return View(model);
        }

        [Authorize]
        public ActionResult Enroll(int classId)   
        {
             var user = (Models.UserModel)Session["User"];
            var course = userCourseManager.Add(user.Id, classId);

            return RedirectToAction("Enrolled");
        }

        [Authorize]
        public ActionResult Enrolled()
        { 
            var user = (Models.UserModel)Session["User"];
           
            var courses = userCourseManager.GetAll(user.Id).Select(t => new Models.UserCourseModel
            {
                UserId = t.UserId,
                ClassId = t.ClassId,
                ClassDescription = t.ClassDescription,
                ClassName = t.ClassName,
                ClassPrice = t.ClassPrice

            }).ToArray();
            
            return View(courses);
        }
    }
}
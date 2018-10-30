using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attendance_ManagementV1._0.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["userType"]==null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["userType"].Equals("Admin"))
            {
               
                return RedirectToAction("Index", "Admin");

            }
            else if(Session["userType"].Equals("Faculty"))
            {
               
                //Faculty Home Page
                return RedirectToAction("Index", "Faculty");

            }
            else if(Session["userType"].Equals("Student"))
            {
                //Student Home Page
                return RedirectToAction("Index", "ShowStudentAttendance");
            }
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
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}
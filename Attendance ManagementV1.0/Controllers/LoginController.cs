using Attendance_ManagementV1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attendance_ManagementV1._0.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        ProjDbContext db = new ProjDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(login l)
        {
            String usertype = l.usertype.ToString();
            string userName = l.username;
            String password = l.password.ToString();
            if (usertype.Equals("Admin"))
            {
                Admin s = db.Admins.Where(x => x.username == userName && x.Password == password).FirstOrDefault();
                var userId = db.Admins.Where(x => x.username == userName).Select(x => x.AdminId).FirstOrDefault();
                if (s == null)
                {
                    ViewBag.ErrorMessage = "Please provide correct credentials!!";
                    return RedirectToAction("Index","Login"); // change karvanu chhe
                }
                else
                {
                    Session["userId"] = userId;
                    Session["userName"] = userName;
                    Session["userType"] = usertype;
                    //return View("Index");
                }
                return RedirectToAction("Index", "Admin");
            } 
            else if (usertype.Equals("Student"))
            {
                
                Student s=db.Students.Where(x => x.username == userName && x.password == password).FirstOrDefault();
                var userId = db.Students.Where(x => x.username == userName).Select(x => x.StudentId).FirstOrDefault();
                if (s == null)
                {
                    ViewBag.ErrorMessage = "Please provide correct credentials!!";
                    return View("Index");
                }
                else
                {
                    Session["userId"] = userId;
                    Session["userType"] = usertype;
                    Session["userName"] = userName;
                    return RedirectToAction("Index", "ShowStudentAttendance");
                }
            }
            else if(usertype.Equals("Faculty"))
            {
               
                Faculty f = db.Faculties.Where(x => x.username == userName && x.password == password).FirstOrDefault();
                var userId = db.Faculties.Where(x => x.username == userName).Select(x => x.FacultyId).FirstOrDefault();
                if (f == null)
                {
                    ViewBag.ErrorMessage = "Please provide correct credentials!!";
                    return View("Index");
                }
                else
                {
                    Session["userId"] = userId;
                    Session["userType"] = usertype;
                    Session["userName"] = userName;
                    return RedirectToAction("Index", "Faculty");
                }
            }
            return View();
          
        }
        public ActionResult InvalidLogin()
        {
            return View();
        }
       

    
    }
}
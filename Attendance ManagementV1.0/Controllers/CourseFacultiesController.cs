using Attendance_ManagementV1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attendance_ManagementV1._0.Controllers
{
    public class CourseFacultiesController : Controller
    {
        // GET: CourseFaculties
        ProjDbContext db = new ProjDbContext();
        public ActionResult Index()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.FacultyCourses);
        }
        public ActionResult Delete(int? id1, int? id2,int? id3)
        {

            if (id1 == null || id2 == null || id3 == null)
            {
                return RedirectToAction("Index");
            }
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            
            FacultyCourse fd1 = db.FacultyCourses.Where(fd => fd.CourseId == id1 && fd.FacultyId == id2 && fd.DivisionId ==id3).FirstOrDefault();
            return View(fd1);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteFacultyCourse(int id1, int id2,int id3)
        {
            FacultyCourse fd1 = db.FacultyCourses.Where(fd => fd.CourseId == id1 && fd.FacultyId == id2 && fd.DivisionId ==id3).FirstOrDefault();

            db.FacultyCourses.Remove(fd1);
            db.SaveChanges();
            FacultyDivision fd2 = db.FacultyDivisions.Where(fd => fd.FacultyId == id2 && fd.DivisionId == id3).FirstOrDefault();
            db.FacultyDivisions.Remove(fd2);
            db.SaveChanges();
            var count = db.FacultyCourses.Where(fd => fd.CourseId == id1 && fd.DivisionId == id3).Count();
            if(count==0)
            {
                DivisionCourse fd = db.DivisionCourses.Where(fd3=>fd3.DivisionId==id3 && fd3.CourseId==id1).FirstOrDefault();
                db.DivisionCourses.Remove(fd);
                db.SaveChanges();
                
            }  

            return RedirectToAction("Index");
        }
    }
}
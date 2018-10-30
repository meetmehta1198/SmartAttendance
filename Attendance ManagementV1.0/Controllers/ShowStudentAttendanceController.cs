using Attendance_ManagementV1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attendance_ManagementV1._0.Controllers
{
    public class DivisionIdName
    {
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
    }
    public class ShowStudentAttendanceController : Controller
    {
        ProjDbContext db = new ProjDbContext();
        // GET: ShowStudentAttendance
        public ActionResult ShowDivisions(int id)
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Student")))
            {
                return RedirectToAction("Index", "Login");
            }
            //List<String> divisionName;
            Dictionary<string, int> TotalCount = new Dictionary<string, int>();
            Dictionary<string, int> TotalLectures = new Dictionary<string, int>();
            int UserId = Int32.Parse(Session["UserId"].ToString());
            //int DivId = db.Students.Where(s => s.StudentId == UserId).FirstOrDefault().DivisionId;
            var CourseIds = db.DivisionCourses.Where(s => s.DivisionId == id).Select(s => s.CourseId);
            foreach (var Cid in CourseIds)
            {
                int t = db.StudentAttendances.Where(s => s.StudentId == UserId && s.CourseId == Cid && s.Status == 1 && s.DivisonId == id).Count();
                string c = db.Courses.Where(s => s.CourseId == Cid).FirstOrDefault().CourseName;
                int t1 = db.StudentAttendances.Where(s => s.StudentId == UserId && s.CourseId == Cid && s.DivisonId == id).Count();
                TotalLectures.Add(c, t1);
                TotalCount.Add(c, t);
            }
            ViewBag.TotalCount = TotalCount;
            ViewBag.TotalLectures = TotalLectures;
            ViewBag.Sname = db.Students.Where(s => s.StudentId == UserId).FirstOrDefault().StudentName;
            return View();
        }
        public ActionResult Index()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Student")))
            {
                return RedirectToAction("Index", "Login");
            }
            var StudentId = Int32.Parse(Session["userId"].ToString());
            IEnumerable<DivisionIdName> divisions = db.StudentAttendances.Where(s => s.StudentId == StudentId).Select(s => new DivisionIdName { DivisionId = s.DivisonId, DivisionName = db.Divisions.Where(div => div.DivisionId == s.DivisonId).FirstOrDefault().DivisionName.ToString() }).Distinct();
            ViewBag.Divisions = divisions;
            return View();
        }
        
    }
}
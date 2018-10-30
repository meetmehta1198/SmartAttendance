using Attendance_ManagementV1._0.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Attendance_ManagementV1._0.Controllers
{
    public class FacultyController : Controller
    {
        // GET: Faculty
        ProjDbContext db = new ProjDbContext();
        public ActionResult Index()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Faculty")))
            {
                return RedirectToAction("Index", "Login");
            }
            int fid = Int32.Parse(Session["userId"].ToString());
            String fname= db.Faculties.Where(f => f.FacultyId == fid).FirstOrDefault().FacultyName;

            var result1 = from fd in db.FacultyDivisions
                         join f in db.Faculties on fd.FacultyId equals f.FacultyId
                         select new { facultyId=f.FacultyId,divisionId = fd.DivisionId, divisionName = db.Divisions.Where(div => div.DivisionId == fd.DivisionId).FirstOrDefault().DivisionName };
       
            ViewBag.DivisionId = new SelectList(result1.Where(r=>r.facultyId==fid),"divisionId","divisionName");
            var result2 = from fc in db.FacultyCourses
                          join f in db.Faculties on fc.FacultyId equals f.FacultyId
                          select new { facultyId = f.FacultyId, courseId = fc.CourseId, courseName = db.Courses.Where(c => c.CourseId == fc.CourseId).FirstOrDefault().CourseName };
            ViewBag.CourseId = new SelectList(result2.Where(res => res.facultyId == fid), "courseId", "courseName");
            //ViewBag.Semesters=new SelectList()
            ViewBag.Fname = fname;
            return View();
        }
        public ActionResult TakeAttendance(int? DivisionId,int? CourseId,DateTime? news_date)
        {
            if(DivisionId == null || CourseId == null || news_date == null)
            {
                return RedirectToAction("Index");
            }
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Faculty")))
            {
                return RedirectToAction("Index", "Login");
            }
            TempData["CourseId"] = CourseId;
            TempData["DivisionId"] = DivisionId;
            TempData["Date"] = news_date;
            return View(db.Students.Where(s => s.DivisionId == DivisionId));
        }
        public ActionResult SubmitAttendance(int[] attendance)
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Faculty")))
            {
                return RedirectToAction("Index", "Login");
            }
            if (attendance != null)
            {
                foreach (var sid in attendance)
                {
                    StudentAttendance s = new StudentAttendance();
                    s.StudentId = sid;
                    s.CourseId = Int32.Parse(TempData["CourseId"].ToString());
                    s.Status = 1;
                    s.date = DateTime.Parse(TempData["Date"].ToString());
                    s.DivisonId = Int32.Parse(TempData["DivisionId"].ToString());
                   // s.date = DateTime.Parse(DateTime.Now.Date.ToShortDateString());
                    db.StudentAttendances.Add(s);
                    db.SaveChanges();


                }
            }
            int did = Int32.Parse(TempData["DivisionId"].ToString());
            var result= db.Students.Where(s => s.DivisionId == did).Select(s => s.StudentId);
            
            if (attendance != null)
            {
                result = result.Except(attendance);
            }
            
            foreach (var id in result.ToList())
            {
                StudentAttendance s = new StudentAttendance();
                s.StudentId = id;
                s.CourseId = Int32.Parse(TempData["CourseId"].ToString());
                s.Status = 0;
                s.date = DateTime.Parse(DateTime.Now.Date.ToShortDateString());
                s.DivisonId = Int32.Parse(TempData["DivisionId"].ToString());
                db.StudentAttendances.Add(s);
                db.SaveChanges();
            }
            TempData.Keep();
            return RedirectToAction("ShowAttendances");
        }
        public ActionResult EditAttendance(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Faculty")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.StudentAttendances.Where(sa=>sa.StudentAttendenceId==id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult EditAttendance(StudentAttendance s)
        {
            StudentAttendance sa = db.StudentAttendances.Where(saa => saa.StudentAttendenceId == s.StudentAttendenceId).FirstOrDefault();
            sa.Status = s.Status;
            db.Entry(sa).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ShowAttendances");
        }
        public ActionResult ShowAttendances(string SearchCourseName,DateTime? SearchDate,int? page)
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Faculty")))
            {
                return RedirectToAction("Index", "Login");
            }
            var sa = from c in db.StudentAttendances
                     orderby c.StudentAttendenceId
                     select c;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if(!string.IsNullOrEmpty(SearchCourseName))
            {
                sa =sa.Where(x => x.courses.CourseName.Contains(SearchCourseName)).OrderBy(x=>x.StudentAttendenceId);
            }
            if (!string.IsNullOrEmpty(SearchDate.ToString()))
            {
                sa = sa.Where(x => x.date == SearchDate).OrderBy(x=>x.StudentAttendenceId);
            }
            return View(sa.ToPagedList(pageNumber,pageSize));
        }




    }
}
using Attendance_ManagementV1._0.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attendance_ManagementV1._0.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        ProjDbContext db = new ProjDbContext();
        public ActionResult Index()
        {
            if(Session["userType"]==null || (Session["userType"]!=null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        public ActionResult AssignCoursesToFaculties()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Message = TempData["Message"];
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName");
            ViewBag.Faculties = db.Faculties;
            return View();
        }
        [HttpPost]
        public ActionResult AssignCoursesToFaculties(int CourseId,int []FacultyIds,int DivisionId)
        {
            bool flag = false;
            TempData["Message"] = "<ul>";
            foreach (int fId in FacultyIds)
            {
                if (db.FacultyCourses.Where(fd => fd.CourseId==CourseId && fd.FacultyId == fId && fd.DivisionId == DivisionId).FirstOrDefault() != null)
                {
                    TempData["Message"] += "<li>" + "Faculty " + db.Faculties.Where(f => f.FacultyId == fId).FirstOrDefault().FacultyName + " Is Already Assigned To " + db.Courses.Where(d => d.CourseId== CourseId).FirstOrDefault().CourseName + ". " + "</li>";
                    flag = true;
                }
                if(!flag)
                {
                    FacultyCourse fCourse = new FacultyCourse();
                    fCourse.CourseId = CourseId;
                    fCourse.FacultyId = fId;
                    fCourse.DivisionId = DivisionId;
                    db.FacultyCourses.Add(fCourse);
                    db.SaveChanges();
                    try
                    {
                        FacultyDivision fDivision = new FacultyDivision();
                        fDivision.FacultyId = fId;
                        fDivision.DivisionId = DivisionId;
                        db.FacultyDivisions.Add(fDivision);
                        db.SaveChanges();
                    }catch(Exception e)
                    {

                    }
                    
                    try
                    {

                        DivisionCourse dCourse = new DivisionCourse();
                        dCourse.CourseId = CourseId;
                        dCourse.DivisionId = DivisionId;
                        db.DivisionCourses.Add(dCourse);
                        db.SaveChanges();
                    }
                    catch(Exception e)
                    {
                        
                    }

                    

                }
            }
            if (flag)
            {
                TempData["Message"] += "</ul>";
                return RedirectToAction("AssignCoursesToFaculties");
            }
            return RedirectToAction("Index","CourseFaculties");
        }
        public ActionResult AssignFacultyToDivisions()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Message = TempData["Message"];
            }
            
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName");
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");
           
            ViewBag.Faculties = db.FacultyCourses;
            return View();
        }
        [HttpPost]
        public ActionResult AssignFacultyToDivisions(int DivisionId,int CourseId, int[] FacultyIds)
        {
            bool flag= false;
            TempData["Message"]="<ul>";
            foreach (int fId in FacultyIds)
            {
                if(db.FacultyDivisions.Where(fd=>fd.DivisionId == DivisionId && fd.FacultyId==fId ).FirstOrDefault()!=null)
                {
                    TempData["Message"]+= "<li>"+"Faculty " + db.Faculties.Where(f=>f.FacultyId==fId).FirstOrDefault().FacultyName + " Is Already Assigned To " + db.Divisions.Where(d=>d.DivisionId ==DivisionId).FirstOrDefault().DivisionName+". "+"for Course Name : "+db.Courses.Where(c=>c.CourseId==CourseId).FirstOrDefault().CourseName+"</li>";
                    flag = true;
                }
                if(!flag)
                {
                    FacultyDivision fDivision = new FacultyDivision();
                    fDivision.DivisionId = DivisionId;
                    fDivision.FacultyId = fId;
                    //fDivision.CourseId = CourseId;
                    db.FacultyDivisions.Add(fDivision);
                    db.SaveChanges();
                }
                
            }
            if (flag)
            {
                TempData["Message"] += "</ul>";
                return RedirectToAction("AssignFacultyToDivisions");
            }
            return RedirectToAction("Index","FacultyDivisions");
        }
        /*public ActionResult ViewFacultyDivisionData()
        {
            return View(db.FacultyDivisions);
        }*/
        public ActionResult CreateFaculty()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateFaculty(Faculty f)
        {
            Faculty fa = new Faculty();
            fa = f;
            db.Faculties.Add(f);
            db.SaveChanges();
            return RedirectToAction("ViewFaculties");
        }
        public ActionResult ViewFaculties()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Faculties);
        }
        public ActionResult ViewDivisions()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Divisions);
        }
        public ActionResult CreateDivision()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateDivision(Division d)
        {
            Division d1 = new Division();
            d1 = d;
            db.Divisions.Add(d1);
            db.SaveChanges();
            return RedirectToAction("ViewDivisions");
        }
        
        public ActionResult ViewCourses()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Courses);
        }
        public ActionResult CreateCourse()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateCourse(Course c)
        {
            Course c1 = new Course();
            c1 = c;
            db.Courses.Add(c1);
            db.SaveChanges();
            return RedirectToAction("ViewCourses");
        }
        public ActionResult EditCourse(int id)
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Courses.Where(c=>c.CourseId==id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult EditCourse(Course c)
        {
            Course c1 = db.Courses.Where(co => co.CourseId == c.CourseId).FirstOrDefault();
            c1.Sem = c.Sem;
            c1.CourseName = c.CourseName;
            db.Entry(c1).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ViewCourses");
        }
        public ActionResult DeleteCourse(int id)
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Courses.Where(c=>c.CourseId==id).FirstOrDefault());
        }
        [HttpPost]
        [ActionName("DeleteCourse")]
        public ActionResult DeleteCourse1(int id)
        {
            Course c1=db.Courses.Where(c => c.CourseId == id).FirstOrDefault();
            db.Courses.Remove(c1);
            db.SaveChanges();
            return RedirectToAction("ViewCourses");

        }
        //public ActionResult EditFaculty(int )
    }
   
}
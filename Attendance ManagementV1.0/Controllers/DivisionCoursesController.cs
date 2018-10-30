using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Attendance_ManagementV1._0.Models;

namespace Attendance_ManagementV1._0.Controllers
{
    public class DivisionCoursesController : Controller
    {
        private ProjDbContext db = new ProjDbContext();

        // GET: DivisionCourses
        public ActionResult Index()
        {
            if (Session["userId"] == null && !Session["userType"].Equals("Admin"))
            {
                RedirectToAction("Index", "Login");
            }
            var divisionCourses = db.DivisionCourses.Include(d => d.courses).Include(d => d.divisions);
            return View(divisionCourses.ToList());
        }

        // GET: DivisionCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["userId"] == null && !Session["userType"].Equals("Admin"))
            {
                RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DivisionCourse divisionCourse = db.DivisionCourses.Find(id);
            if (divisionCourse == null)
            {
                return HttpNotFound();
            }
            return View(divisionCourse);
        }

        // GET: DivisionCourses/Create
        public ActionResult Create()
        {
            if (Session["userId"] == null && !Session["userType"].Equals("Admin"))
            {
                RedirectToAction("Index", "Login");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName");
            return View();
        }

        // POST: DivisionCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DivisionId,CourseId")] DivisionCourse divisionCourse)
        {
            if (ModelState.IsValid)
            {
                                var students = from s in db.Students
                               where s.DivisionId == divisionCourse.DivisionId
                               select s;
                foreach (var s in students)
                {
                    StudentCourse sc = new StudentCourse();
                    sc.StudentId = s.StudentId;
                    sc.CourseId = divisionCourse.CourseId;
                    Console.WriteLine(sc);
                    db.StudentCourses.Add(sc);
                   
                }
                db.SaveChanges();
                db.DivisionCourses.Add(divisionCourse);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
           
             
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", divisionCourse.CourseId);
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionId", divisionCourse.DivisionId);
            return View(divisionCourse);
        }

        // GET: DivisionCourses/Edit/5
        public ActionResult Edit(int? id1,int? id2)
        {
            if (Session["userId"] == null && !Session["userType"].Equals("Admin"))
            {
                RedirectToAction("Index", "Login");
            }
            if (id1 == null || id2==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DivisionCourse divisionCourse = db.DivisionCourses.Where(s=>s.DivisionId==id1 && s.CourseId==id2).FirstOrDefault();
            if (divisionCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", divisionCourse.CourseId);
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName", divisionCourse.DivisionId);
            return View(divisionCourse);
        }

        // POST: DivisionCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DivisionId,CourseId")] DivisionCourse divisionCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(divisionCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", divisionCourse.CourseId);
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName", divisionCourse.DivisionId);
            return View(divisionCourse);
        }

        // GET: DivisionCourses/Delete/5
        public ActionResult Delete(int? id1,int? id2)
        {
            if (Session["userId"] == null && !Session["userType"].Equals("Admin"))
            {
                RedirectToAction("Index", "Login");
            }
            if (id1 == null || id2==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DivisionCourse divisionCourse = db.DivisionCourses.Where(s=>s.DivisionId==id1 && s.CourseId==id2).FirstOrDefault();
            if (divisionCourse == null)
            {
                return HttpNotFound();
            }
            return View(divisionCourse);
        }

        // POST: DivisionCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id1,int id2)
        {
            DivisionCourse divisionCourse = db.DivisionCourses.Where(s=>s.DivisionId==id1 && s.CourseId==id2).FirstOrDefault();
            db.DivisionCourses.Remove(divisionCourse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

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
    public class StudentCoursesController : Controller
    {
        private ProjDbContext db = new ProjDbContext();

        // GET: StudentCourses
        public ActionResult Index()
        {
            var studentCourses = db.StudentCourses.Include(s => s.courses).Include(s => s.students);
            return View(studentCourses.ToList());
        }

        // GET: StudentCourses/Details/5
        // GET: StudentCourses/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentName");
            return View();
        }

        // POST: StudentCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,CourseId")] StudentCourse studentCourse)
        {
            if (ModelState.IsValid)
            {
                db.StudentCourses.Add(studentCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", studentCourse.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentName", studentCourse.StudentId);
            return View(studentCourse);
        }

        // GET: StudentCourses/Edit/5
        
        // GET: StudentCourses/Delete/5
        public ActionResult Delete(int? id1,int?id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentCourse studentCourse = db.StudentCourses.Where(sc => sc.CourseId == id1 && sc.StudentId == id2).FirstOrDefault();
            if (studentCourse == null)
            {
                return HttpNotFound();
            }
            return View(studentCourse);
        }

        // POST: StudentCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id1,int id2)
        {
            StudentCourse studentCourse = db.StudentCourses.Where(sc => sc.CourseId == id1 && sc.StudentId == id2).FirstOrDefault();
            db.StudentCourses.Remove(studentCourse);
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

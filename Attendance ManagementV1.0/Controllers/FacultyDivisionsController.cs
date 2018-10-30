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
    public class FacultyDivisionsController : Controller
    {
        private ProjDbContext db = new ProjDbContext();

        // GET: FacultyDivisions
        public ActionResult Index()
        {
            var facultyDivisions = db.FacultyDivisions.Include(f => f.divisions).Include(f => f.faculties);
            return View(facultyDivisions.ToList());
        }

        // GET: FacultyDivisions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacultyDivision facultyDivision = db.FacultyDivisions.Find(id);
            if (facultyDivision == null)
            {
                return HttpNotFound();
            }
            return View(facultyDivision);
        }

        // GET: FacultyDivisions/Create
        public ActionResult Create()
        {
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName");
            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "FacultyName");
            return View();
        }

        // POST: FacultyDivisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FacultyId,DivisionId")] FacultyDivision facultyDivision)
        {
            if (ModelState.IsValid)
            {
                db.FacultyDivisions.Add(facultyDivision);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionId", facultyDivision.DivisionId);
            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "FacultyName", facultyDivision.FacultyId);
            return View(facultyDivision);
        }

        // GET: FacultyDivisions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacultyDivision facultyDivision = db.FacultyDivisions.Find(id);
            if (facultyDivision == null)
            {
                return HttpNotFound();
            }
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionId", facultyDivision.DivisionId);
            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "FacultyName", facultyDivision.FacultyId);
            return View(facultyDivision);
        }

        // POST: FacultyDivisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FacultyId,DivisionId")] FacultyDivision facultyDivision)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facultyDivision).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionId", facultyDivision.DivisionId);
            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "FacultyName", facultyDivision.FacultyId);
            return View(facultyDivision);
        }

        // GET: FacultyDivisions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacultyDivision facultyDivision = db.FacultyDivisions.Find(id);
            if (facultyDivision == null)
            {
                return HttpNotFound();
            }
            return View(facultyDivision);
        }

        // POST: FacultyDivisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FacultyDivision facultyDivision = db.FacultyDivisions.Find(id);
            db.FacultyDivisions.Remove(facultyDivision);
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

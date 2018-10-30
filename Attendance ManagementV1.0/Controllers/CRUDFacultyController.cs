using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Attendance_ManagementV1._0.Models
{
    public class CRUDFacultyController : Controller
    {
        private ProjDbContext db = new ProjDbContext();

        // GET: CRUDFaculty
        public ActionResult Index()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Faculties.ToList());
        }

        // GET: CRUDFaculty/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // GET: CRUDFaculty/Create
        public ActionResult Create()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: CRUDFaculty/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                db.Faculties.Add(faculty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(faculty);
        }

        // GET: CRUDFaculty/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // POST: CRUDFaculty/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faculty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faculty);
        }

        // GET: CRUDFaculty/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // POST: CRUDFaculty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Faculty faculty = db.Faculties.Find(id);
            db.Faculties.Remove(faculty);
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

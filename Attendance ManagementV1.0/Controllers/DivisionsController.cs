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
    public class DivisionsController : Controller
    {
        private ProjDbContext db = new ProjDbContext();

        // GET: Divisions
        public ActionResult Index()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Divisions.ToList());
        }

        // GET: Divisions/Details/5
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
            Division division = db.Divisions.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        // GET: Divisions/Create
        public ActionResult Create()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Divisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Division division)
        {
            if (ModelState.IsValid)
            {
                db.Divisions.Add(division);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(division);
        }

        // GET: Divisions/Edit/5
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
            Division division = db.Divisions.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        // POST: Divisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Division division)
        {
            if (ModelState.IsValid)
            {
                db.Entry(division).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(division);
        }

        // GET: Divisions/Delete/5
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
            Division division = db.Divisions.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        // POST: Divisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Division division = db.Divisions.Find(id);
            db.Divisions.Remove(division);
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

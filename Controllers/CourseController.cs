using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proms.Models;

namespace Proms.Controllers
{
    public class CourseController : Controller
    {
        private DbIDEntities db = new DbIDEntities();

        // GET: Course
        public ActionResult Index()
        {
            return View(db.SCourses.ToList());
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SCourse sCourse = db.SCourses.Find(id);
            if (sCourse == null)
            {
                return HttpNotFound();
            }
            return View(sCourse);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Course")] SCourse sCourse)
        {
            if (ModelState.IsValid)
            {
                db.SCourses.Add(sCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sCourse);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SCourse sCourse = db.SCourses.Find(id);
            if (sCourse == null)
            {
                return HttpNotFound();
            }
            return View(sCourse);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Course")] SCourse sCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sCourse);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SCourse sCourse = db.SCourses.Find(id);
            if (sCourse == null)
            {
                return HttpNotFound();
            }
            return View(sCourse);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SCourse sCourse = db.SCourses.Find(id);
            db.SCourses.Remove(sCourse);
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

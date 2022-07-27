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
    public class GradeLevelController : Controller
    {
        private DbIDEntities db = new DbIDEntities();

        // GET: GradeLevel
        public ActionResult Index()
        {
            return View(db.SGrdls.ToList());
        }

        // GET: GradeLevel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SGrdl sGrdl = db.SGrdls.Find(id);
            if (sGrdl == null)
            {
                return HttpNotFound();
            }
            return View(sGrdl);
        }

        // GET: GradeLevel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GradeLevel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,GradeLevel")] SGrdl sGrdl)
        {
            if (ModelState.IsValid)
            {
                db.SGrdls.Add(sGrdl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sGrdl);
        }

        // GET: GradeLevel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SGrdl sGrdl = db.SGrdls.Find(id);
            if (sGrdl == null)
            {
                return HttpNotFound();
            }
            return View(sGrdl);
        }

        // POST: GradeLevel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,GradeLevel")] SGrdl sGrdl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sGrdl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sGrdl);
        }

        // GET: GradeLevel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SGrdl sGrdl = db.SGrdls.Find(id);
            if (sGrdl == null)
            {
                return HttpNotFound();
            }
            return View(sGrdl);
        }

        // POST: GradeLevel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SGrdl sGrdl = db.SGrdls.Find(id);
            db.SGrdls.Remove(sGrdl);
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

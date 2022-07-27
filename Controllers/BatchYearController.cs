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
    public class BatchYearController : Controller
    {
        private DbIDEntities db = new DbIDEntities();

        // GET: BatchYear
        public ActionResult Index()
        {
            return View(db.SBies.ToList());
        }

        // GET: BatchYear/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SBY sBY = db.SBies.Find(id);
            if (sBY == null)
            {
                return HttpNotFound();
            }
            return View(sBY);
        }

        // GET: BatchYear/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BatchYear/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,BatchYear")] SBY sBY)
        {
            if (ModelState.IsValid)
            {
                db.SBies.Add(sBY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sBY);
        }

        // GET: BatchYear/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SBY sBY = db.SBies.Find(id);
            if (sBY == null)
            {
                return HttpNotFound();
            }
            return View(sBY);
        }

        // POST: BatchYear/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,BatchYear")] SBY sBY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sBY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sBY);
        }

        // GET: BatchYear/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SBY sBY = db.SBies.Find(id);
            if (sBY == null)
            {
                return HttpNotFound();
            }
            return View(sBY);
        }

        // POST: BatchYear/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SBY sBY = db.SBies.Find(id);
            db.SBies.Remove(sBY);
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

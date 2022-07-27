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
    public class PositionController : Controller
    {
        private DbIDEntities db = new DbIDEntities();

        // GET: Position
        public ActionResult Index()
        {
            return View(db.EPositions.ToList());
        }

        // GET: Position/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EPosition ePosition = db.EPositions.Find(id);
            if (ePosition == null)
            {
                return HttpNotFound();
            }
            return View(ePosition);
        }

        // GET: Position/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Position/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Position")] EPosition ePosition)
        {
            if (ModelState.IsValid)
            {
                db.EPositions.Add(ePosition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ePosition);
        }

        // GET: Position/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EPosition ePosition = db.EPositions.Find(id);
            if (ePosition == null)
            {
                return HttpNotFound();
            }
            return View(ePosition);
        }

        // POST: Position/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Position")] EPosition ePosition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ePosition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ePosition);
        }

        // GET: Position/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EPosition ePosition = db.EPositions.Find(id);
            if (ePosition == null)
            {
                return HttpNotFound();
            }
            return View(ePosition);
        }

        // POST: Position/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EPosition ePosition = db.EPositions.Find(id);
            db.EPositions.Remove(ePosition);
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

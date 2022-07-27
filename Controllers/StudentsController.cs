using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proms.Models;

namespace Proms.Controllers
{
    public class StudentsController : Controller
    {
        private DbIDEntities db = new DbIDEntities();

        // GET: Student
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.SBY).Include(s => s.SCourse).Include(s => s.SGrdl).Include(s => s.Status1);
            return View(students.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        //GET : Students/Details/Generate
        public ActionResult Generate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

       /* [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Image image)
        {
            string fileName = Path.GetFileNameWithoutExtension(image.imageFile.FileName);
            string extension = Path.GetExtension(image.imageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            image.ImagePic = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            image.ImageFile.SaveAs(fileName);
            using (FinIDDbEntities db = new FinIDDbEntities())
            {
                db.Students.Add(image);
                db.SaveChanges();
            }
            ModelState.Clear();

            return View();
        }*/

        // GET: Student/Create
        public ActionResult Create()
        {

            ViewBag.BatchYear = new SelectList(db.SBies, "id", "BatchYear");
            ViewBag.Course = new SelectList(db.SCourses, "id", "Course");
            ViewBag.GradeLevel = new SelectList(db.SGrdls, "id", "GradeLevel");
            ViewBag.Status = new SelectList(db.Status, "id", "Status1");
            return View();
        }
       
        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,StudentID,Firstname,MI,Lastname,Course,GradeLevel,BatchYear,Parent,Address,ContractNo,Status,ImagePic,Signature")] Student student)
        {
           
            if (ModelState.IsValid)
            { 
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BatchYear = new SelectList(db.SBies, "id", "BatchYear", student.BatchYear);
            ViewBag.Course = new SelectList(db.SCourses, "id", "Course", student.Course);
            ViewBag.GradeLevel = new SelectList(db.SGrdls, "id", "GradeLevel", student.GradeLevel);
            ViewBag.Status = new SelectList(db.Status, "id", "Status1", student.Status);
            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.BatchYear = new SelectList(db.SBies, "id", "BatchYear", student.BatchYear);
            ViewBag.Course = new SelectList(db.SCourses, "id", "Course", student.Course);
            ViewBag.GradeLevel = new SelectList(db.SGrdls, "id", "GradeLevel", student.GradeLevel);
            ViewBag.Status = new SelectList(db.Status, "id", "Status1", student.Status);
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,StudentID,Firstname,MI,Lastname,Course,GradeLevel,BatchYear,Parent,Address,ContractNo,Status,ImagePic,Signature")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BatchYear = new SelectList(db.SBies, "id", "BatchYear", student.BatchYear);
            ViewBag.Course = new SelectList(db.SCourses, "id", "Course", student.Course);
            ViewBag.GradeLevel = new SelectList(db.SGrdls, "id", "GradeLevel", student.GradeLevel);
            ViewBag.Status = new SelectList(db.Status, "id", "Status1", student.Status);
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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

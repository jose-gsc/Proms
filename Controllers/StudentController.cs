using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proms.Models;

namespace Proms.Controllers
{
    public class StudentController : Controller
    {
        private DbIDEntities db = new DbIDEntities();

        // GET: Student
        public ActionResult Index()
        {
            ViewBag.Status = new SelectList(db.Status, "id", "Status1");
            var students = db.Students.Include(s => s.SBY).Include(s => s.SCourse).Include(s => s.SGrdl).Include(s => s.Status1);
            return View(students.ToList());
        }
        public ActionResult Fin()
        {
            var students = db.Students.Include(s => s.SBY).Include(s => s.SCourse).Include(s => s.SGrdl).Include(s => s.Status1);
            return View(students.ToList());
        }

        // GET: Student/FinEdit/5
        public ActionResult FinEdit(int? id)
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
        // POST: Student/FinEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinEdit([Bind(Include = "id,StudentID,Firstname,MI,Lastname,Course,GradeLevel,BatchYear,Parent,Address,Contact,Status,ImagePic,Signature")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Fin");
            }
            ViewBag.BatchYear = new SelectList(db.SBies, "id", "BatchYear", student.BatchYear);
            ViewBag.Course = new SelectList(db.SCourses, "id", "Course", student.Course);
            ViewBag.GradeLevel = new SelectList(db.SGrdls, "id", "GradeLevel", student.GradeLevel);
            ViewBag.Status = new SelectList(db.Status, "id", "Status1", student.Status);
            return View(student);
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
        public ActionResult Create([Bind(Include = "id,StudentID,Firstname,MI,Lastname,Course,GradeLevel,BatchYear,Parent,Address,Contact,Status,ImagePic,Signature")] Student student, HttpPostedFileBase imgfile)
        {
            string path = uploadimage(imgfile);
            /*  Student di = new Student();*/
           
            if (ModelState.IsValid)
            {
                student.ImagePic = path;
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            /* if (path.Equals("-1"))
             {
             }
             else
             {
                 di.StudentID = student.StudentID;
                 di.Firstname = student.Firstname;
                 di.MI = student.MI;
                 di.Lastname = student.Lastname;
                 di.Course = student.Course;
                 di.GradeLevel = student.GradeLevel;
                 di.Parent = student.Parent;
                 di.Address = student.Address;
                 di.Contact = student.Contact;
                 di.Status = student.Status;
                 di.ImagePic = path;
                 db.Students.Add(di);
                 db.SaveChanges();
               *//*  return RedirectToAction("Index");*//*

             }*/
            ViewBag.BatchYear = new SelectList(db.SBies, "id", "BatchYear", student.BatchYear);
            ViewBag.Course = new SelectList(db.SCourses, "id", "Course", student.Course);
            ViewBag.GradeLevel = new SelectList(db.SGrdls, "id", "GradeLevel", student.GradeLevel);
            ViewBag.Status = new SelectList(db.Status, "id", "Status1", student.Status);
            return View(student);
        }

        public string uploadimage(HttpPostedFileBase file)
        {
            string path = "-1";
            Random r = new Random();
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/upload"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/upload/" + random + Path.GetFileName(file.FileName);
                        //    ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }

            return path;
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
        public ActionResult Edit([Bind(Include = "id,StudentID,Firstname,MI,Lastname,Course,GradeLevel,BatchYear,Parent,Address,Contact,Status,ImagePic,Signature")] Student student)
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

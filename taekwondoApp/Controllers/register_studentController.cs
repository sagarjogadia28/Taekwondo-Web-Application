using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using taekwondoApp.Models;

namespace taekwondoApp.Controllers
{
    public class register_studentController : Controller
    {
        private taekwondoDBEntities db = new taekwondoDBEntities();

        // GET: register_student
        public ActionResult Index(string sortOrder, string searchString)
        {

			ViewBag.EmailSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			var register_students = from r in db.register_student.Include(r => r.student_class).Include(r => r.student)
									select r;
			if (!String.IsNullOrEmpty(searchString))
			{
				register_students = register_students.Where(s => s.student.email_id.Contains(searchString));
			}
			switch (sortOrder)
			{
				case "name_desc":
					register_students = register_students.OrderByDescending(s => s.student.email_id);
					break;
				default:
					register_students = register_students.OrderBy(s => s.student.email_id);
					break;
			}

			//var register_student = db.register_student.Include(r => r.student_class).Include(r => r.student);
            return View(register_students.ToList());
        }

        // GET: register_student/Details/5
        public ActionResult Details(int? id, int? stId, int? classId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            register_student register_student = db.register_student.Find(id,stId,classId);
            if (register_student == null)
            {
                return HttpNotFound();
            }
            return View(register_student);
        }

        // GET: register_student/Create
        public ActionResult Create()
        {
			ViewBag.class_id = new SelectList(db.student_class, "class_id", "getDetails");
            ViewBag.student_id = new SelectList(db.students, "student_id", "email_id");
            return View();
        }

        // POST: register_student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "register_id,student_id,class_id")] register_student register_student)
        {
            if (ModelState.IsValid)
            {
                db.register_student.Add(register_student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.class_id = new SelectList(db.student_class, "class_id", "getDetails", register_student.class_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "email_id", register_student.student_id);
            return View(register_student);
        }

        // GET: register_student/Edit/5
        public ActionResult Edit(int? id, int? stId, int? classId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            register_student register_student = db.register_student.Find(id,stId,classId);
            if (register_student == null)
            {
                return HttpNotFound();
            }
            ViewBag.class_id = new SelectList(db.student_class, "class_id", "getDetails", register_student.class_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "email_id", register_student.student_id);
            return View(register_student);
        }

        // POST: register_student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "register_id,student_id,class_id")] register_student register_student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(register_student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.class_id = new SelectList(db.student_class, "class_id", "getDetails", register_student.class_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "email_id", register_student.student_id);
            return View(register_student);
        }

        // GET: register_student/Delete/5
        public ActionResult Delete(int? id, int? stId, int? classId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            register_student register_student = db.register_student.Find(id, stId, classId);
            if (register_student == null)
            {
                return HttpNotFound();
            }
            return View(register_student);
        }

        // POST: register_student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int stId, int classId)
        {
            register_student register_student = db.register_student.Find(id, stId, classId);
            db.register_student.Remove(register_student);
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

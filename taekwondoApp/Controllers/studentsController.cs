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
    public class studentsController : Controller
    {
        private taekwondoDBEntities db = new taekwondoDBEntities();

        // GET: students
        public ActionResult Index(string sortOrder, string searchString)
        {
			ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_name_desc" : "";
			ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "last_name_desc" : "last_name";
			ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
			var students = from s in db.students
						   select s;

			if (!String.IsNullOrEmpty(searchString))
			{
				students = students.Where(s => s.last_name.Contains(searchString)
									   || s.first_name.Contains(searchString)
									   || s.middle_name.Contains(searchString)
									   || s.date_of_joining.ToString().Contains(searchString)
									   || s.date_of_birth.ToString().Contains(searchString)
									   || s.mobile_number.Contains(searchString)
									   || s.email_id.Contains(searchString)
									   || s.street.Contains(searchString)
									   || s.city.Contains(searchString)
									   || s.province.Contains(searchString)
									   || s.postal_code.Contains(searchString));
			}

			switch (sortOrder)
			{
				case "first_name_desc":
					students = students.OrderByDescending(s => s.first_name);
					break;
				case "last_name_desc":
					students = students.OrderByDescending(s => s.last_name);
					break;
				case "last_name":
					students = students.OrderBy(s => s.last_name);
					break;
				case "Date":
					students = students.OrderBy(s => s.date_of_joining);
					break;
				case "date_desc":
					students = students.OrderByDescending(s => s.date_of_joining);
					break;
				default:
					students = students.OrderBy(s => s.first_name);
					break;
			}


			//var students = db.students.Include(s => s.parent);
            return View(students.ToList());
        }

        // GET: students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: students/Create
        public ActionResult Create()
        {
            ViewBag.parent_id = new SelectList(db.parents, "parent_id", "parent_email_id");
            return View();
        }

        // POST: students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "student_id,first_name,last_name,middle_name,date_of_birth,date_of_joining,mobile_number,email_id,street,city,postal_code,province,is_active,parent_id")] student student)
        {
            if (ModelState.IsValid)
            {
                db.students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.parent_id = new SelectList(db.parents, "parent_id", "father_name", student.parent_id);
            return View(student);
        }

        // GET: students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.parent_id = new SelectList(db.parents, "parent_id", "father_name", student.parent_id);
            return View(student);
        }

        // POST: students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "student_id,first_name,last_name,middle_name,date_of_birth,date_of_joining,mobile_number,email_id,street,city,postal_code,province,is_active,parent_id")] student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.parent_id = new SelectList(db.parents, "parent_id", "father_name", student.parent_id);
            return View(student);
        }

        // GET: students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            student student = db.students.Find(id);
            db.students.Remove(student);
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

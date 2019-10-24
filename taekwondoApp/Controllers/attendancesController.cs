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
    public class attendancesController : Controller
    {
        private taekwondoDBEntities db = new taekwondoDBEntities();

        // GET: attendances
        public ActionResult Index(string sortOrder, string searchString)
        {
			ViewBag.EmailSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.AttendSortParm = String.IsNullOrEmpty(sortOrder) ? "attend_desc" : "attend";

			var attendances = from a in db.attendances.Include(a => a.student_class).Include(a => a.student)
									select a;
			if (!String.IsNullOrEmpty(searchString))
			{
				attendances = attendances.Where(s => s.student.email_id.Contains(searchString));
			}
			switch (sortOrder)
			{
				case "name_desc":
					attendances = attendances.OrderByDescending(s => s.student.email_id);
					break;
				case "attend_desc":
					attendances = attendances.OrderByDescending(s => s.did_attend);
					break;
				case "attend":
					attendances = attendances.OrderBy(s => s.did_attend);
					break;
				default:
					attendances = attendances.OrderBy(s => s.student.email_id);
					break;
			}

			//var attendances = db.attendances.Include(a => a.student_class).Include(a => a.student);
            return View(attendances.ToList());
        }

        // GET: attendances/Details/5
        public ActionResult Details(int? id, int? classId, DateTime dt)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attendance attendance = db.attendances.Find(id,classId,dt);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: attendances/Create
        public ActionResult Create()
        {
            ViewBag.class_id = new SelectList(db.student_class, "class_id", "getDetails");
            ViewBag.student_id = new SelectList(db.students, "student_id", "email_id");
            return View();
        }

        // POST: attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "student_id,class_id,class_date,did_attend")] attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.class_id = new SelectList(db.student_class, "class_id", "getDetails", attendance.class_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "email_id", attendance.student_id);
            return View(attendance);
        }

        // GET: attendances/Edit/5
        public ActionResult Edit(int? id, int? classId, DateTime dt)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attendance attendance = db.attendances.Find(id,classId,dt);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.class_id = new SelectList(db.student_class, "class_id", "getDetails", attendance.class_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "email_id", attendance.student_id);
            return View(attendance);
        }

        // POST: attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "student_id,class_id,class_date,did_attend")] attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.class_id = new SelectList(db.student_class, "class_id", "getDetails", attendance.class_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "email_id", attendance.student_id);
            return View(attendance);
        }

        // GET: attendances/Delete/5
        public ActionResult Delete(int? id, int? classId, DateTime dt)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attendance attendance = db.attendances.Find(id,classId,dt);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int classId, DateTime dt)
        {
            attendance attendance = db.attendances.Find(id,classId,dt);
            db.attendances.Remove(attendance);
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

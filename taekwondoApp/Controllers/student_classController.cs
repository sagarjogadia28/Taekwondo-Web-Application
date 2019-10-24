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
    public class student_classController : Controller
    {
        private taekwondoDBEntities db = new taekwondoDBEntities();

        // GET: student_class
        public ActionResult Index(string sortOrder, string searchString)
        {
			ViewBag.LevelSortParm = String.IsNullOrEmpty(sortOrder) ? "class_level_desc" : "";
			var student_class = from s in db.student_class
								select s;

			if (!String.IsNullOrEmpty(searchString))
			{
				student_class = student_class.Where(s => s.class_level.Contains(searchString)
									   || s.class_on.Contains(searchString)
									   || s.end_time.ToString().Contains(searchString)
									   || s.start_time.ToString().Contains(searchString));
			}

			switch (sortOrder)
			{
				case "class_level_desc":
					student_class = student_class.OrderByDescending(s => s.class_level);
					break;
				default:
					student_class = student_class.OrderBy(s => s.class_level);
					break;
			}


			return View(student_class.ToList());
        }

        // GET: student_class/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student_class student_class = db.student_class.Find(id);
            if (student_class == null)
            {
                return HttpNotFound();
            }
            return View(student_class);
        }

        // GET: student_class/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: student_class/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "class_id,class_level,class_on,start_time,end_time")] student_class student_class)
        {
            if (ModelState.IsValid)
            {
                db.student_class.Add(student_class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student_class);
        }

        // GET: student_class/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student_class student_class = db.student_class.Find(id);
            if (student_class == null)
            {
                return HttpNotFound();
            }
            return View(student_class);
        }

        // POST: student_class/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "class_id,class_level,class_on,start_time,end_time")] student_class student_class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student_class);
        }

        // GET: student_class/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student_class student_class = db.student_class.Find(id);
            if (student_class == null)
            {
                return HttpNotFound();
            }
            return View(student_class);
        }

        // POST: student_class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            student_class student_class = db.student_class.Find(id);
            db.student_class.Remove(student_class);
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

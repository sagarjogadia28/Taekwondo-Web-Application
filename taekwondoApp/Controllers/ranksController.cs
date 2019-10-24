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
    public class ranksController : Controller
    {
        private taekwondoDBEntities db = new taekwondoDBEntities();

        // GET: ranks
        public ActionResult Index(string sortOrder, string searchString)
		{
			ViewBag.EmailSortParm = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";
			ViewBag.BeltSortParm = String.IsNullOrEmpty(sortOrder) ? "belt_desc" : "belt";
			ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "date";
			var ranks = from r in db.ranks.Include(r => r.student)
						select r;

			if (!String.IsNullOrEmpty(searchString))
			{
				ranks = ranks.Where(r => r.student.email_id.Contains(searchString)
									   || r.belt_color.Contains(searchString)
									   || r.date_awarded.ToString().Contains(searchString));
			}

			switch (sortOrder)
			{
				case "email_desc":
					ranks = ranks.OrderByDescending(r => r.student.email_id);
					break;
				case "belt_desc":
					ranks = ranks.OrderByDescending(r => r.belt_color);
					break;
				case "belt":
					ranks = ranks.OrderBy(r => r.belt_color);
					break;
				case "date":
					ranks = ranks.OrderBy(r => r.date_awarded);
					break;
				case "date_desc":
					ranks = ranks.OrderByDescending(r => r.date_awarded);
					break;
				default:
					ranks = ranks.OrderBy(r => r.student.email_id);
					break;
			}

			//var ranks = db.ranks.Include(r => r.student);
            return View(ranks.ToList());
        }

        // GET: ranks/Details/5
        public ActionResult Details(int? id, string color)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rank rank = db.ranks.Find(id,color);
            if (rank == null)
            {
                return HttpNotFound();
            }
            return View(rank);
        }

        // GET: ranks/Create
        public ActionResult Create()
        {
            ViewBag.student_id = new SelectList(db.students, "student_id", "email_id");
            return View();
        }

        // POST: ranks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "student_id,belt_color,date_awarded")] rank rank)
        {
            if (ModelState.IsValid)
            {
                db.ranks.Add(rank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.student_id = new SelectList(db.students, "student_id", "email_id", rank.student_id);
            return View(rank);
        }

        // GET: ranks/Edit/5
        public ActionResult Edit(int? id, string color)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rank rank = db.ranks.Find(id,color);
            if (rank == null)
            {
                return HttpNotFound();
            }
            ViewBag.student_id = new SelectList(db.students, "student_id", "email_id", rank.student_id);
            return View(rank);
        }

        // POST: ranks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "student_id,belt_color,date_awarded")] rank rank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.student_id = new SelectList(db.students, "student_id", "email_id", rank.student_id);
            return View(rank);
        }

        // GET: ranks/Delete/5
        public ActionResult Delete(int? id, string color)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rank rank = db.ranks.Find(id,color);
            if (rank == null)
            {
                return HttpNotFound();
            }
            return View(rank);
        }

        // POST: ranks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,string color)
        {
            rank rank = db.ranks.Find(id,color);
            db.ranks.Remove(rank);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using taekwondoApp.Models;

namespace taekwondoApp.Controllers
{
    public class money_trackingController : Controller
    {
        private taekwondoDBEntities db = new taekwondoDBEntities();

        // GET: money_tracking
        public ActionResult Index(string startdate = null, string enddate = null)
        {
			if (startdate != null && enddate != null)
			{
				DateTime d1, d2;
				d1 = Convert.ToDateTime(startdate);
				d2 = Convert.ToDateTime(enddate);
				//this will default to current date if for whatever reason the date supplied by user did not parse successfully
				var rangeData = db.money_tracking.Where(x => x.date_of_purchase >= d1 && x.date_of_purchase <= d2);
				return View(rangeData.ToList());
			}
			else {
				var money_tracking = db.money_tracking.Include(m => m.inventory).Include(m => m.student);
				return View(money_tracking.ToList());
			}			
			//return View();
		}

        // GET: money_tracking/Details/5
        public ActionResult Details(int? id, int? inId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            money_tracking money_tracking = db.money_tracking.Find(id, inId);
            if (money_tracking == null)
            {
                return HttpNotFound();
            }
            return View(money_tracking);
        }

        // GET: money_tracking/Create
        public ActionResult Create()
        {
            ViewBag.inventory_id = new SelectList(db.inventories, "inventory_id", "name");
            ViewBag.student_id = new SelectList(db.students, "student_id", "email_id");
            return View();
        }

        // POST: money_tracking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "student_id,inventory_id,date_of_purchase,amount")] money_tracking money_tracking)
        {
            if (ModelState.IsValid)
            {
                db.money_tracking.Add(money_tracking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.inventory_id = new SelectList(db.inventories, "inventory_id", "name", money_tracking.inventory_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "email_id", money_tracking.student_id);
            return View(money_tracking);
        }

        // GET: money_tracking/Edit/5
        public ActionResult Edit(int? id, int? inId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            money_tracking money_tracking = db.money_tracking.Find(id, inId);
            if (money_tracking == null)
            {
                return HttpNotFound();
            }
            ViewBag.inventory_id = new SelectList(db.inventories, "inventory_id", "name", money_tracking.inventory_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "email_id", money_tracking.student_id);
            return View(money_tracking);
        }

        // POST: money_tracking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "student_id,inventory_id,date_of_purchase,amount")] money_tracking money_tracking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(money_tracking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.inventory_id = new SelectList(db.inventories, "inventory_id", "name", money_tracking.inventory_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "email_id", money_tracking.student_id);
            return View(money_tracking);
        }

        // GET: money_tracking/Delete/5
        public ActionResult Delete(int? id, int? inId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            money_tracking money_tracking = db.money_tracking.Find(id, inId);
            if (money_tracking == null)
            {
                return HttpNotFound();
            }
            return View(money_tracking);
        }

        // POST: money_tracking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int inId)
        {
            money_tracking money_tracking = db.money_tracking.Find(id, inId);
            db.money_tracking.Remove(money_tracking);
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

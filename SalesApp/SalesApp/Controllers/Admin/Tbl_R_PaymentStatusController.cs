using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using web.Models;

namespace web.Controllers.Admin
{
    [Authorize]
    public class Tbl_R_PaymentStatusController : Controller
    {
        private ValkiriaReservasEntities db = new ValkiriaReservasEntities();

        // GET: Tbl_R_PaymentStatus
        public ActionResult Index()
        {
            return View(db.Tbl_R_PaymentStatus.ToList());
        }

        // GET: Tbl_R_PaymentStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_R_PaymentStatus tbl_R_PaymentStatus = db.Tbl_R_PaymentStatus.Find(id);
            if (tbl_R_PaymentStatus == null)
            {
                return HttpNotFound();
            }
            return View(tbl_R_PaymentStatus);
        }

        // GET: Tbl_R_PaymentStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tbl_R_PaymentStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LangId,PaymentStatusId")] Tbl_R_PaymentStatus tbl_R_PaymentStatus)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_R_PaymentStatus.Add(tbl_R_PaymentStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_R_PaymentStatus);
        }

        // GET: Tbl_R_PaymentStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_R_PaymentStatus tbl_R_PaymentStatus = db.Tbl_R_PaymentStatus.Find(id);
            if (tbl_R_PaymentStatus == null)
            {
                return HttpNotFound();
            }
            return View(tbl_R_PaymentStatus);
        }

        // POST: Tbl_R_PaymentStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LangId,PaymentStatusId")] Tbl_R_PaymentStatus tbl_R_PaymentStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_R_PaymentStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_R_PaymentStatus);
        }

        // GET: Tbl_R_PaymentStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_R_PaymentStatus tbl_R_PaymentStatus = db.Tbl_R_PaymentStatus.Find(id);
            if (tbl_R_PaymentStatus == null)
            {
                return HttpNotFound();
            }
            return View(tbl_R_PaymentStatus);
        }

        // POST: Tbl_R_PaymentStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_R_PaymentStatus tbl_R_PaymentStatus = db.Tbl_R_PaymentStatus.Find(id);
            db.Tbl_R_PaymentStatus.Remove(tbl_R_PaymentStatus);
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

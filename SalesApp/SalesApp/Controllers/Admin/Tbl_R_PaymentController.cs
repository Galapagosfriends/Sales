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
    public class Tbl_R_PaymentController : Controller
    {
        private ValkiriaReservasEntities db = new ValkiriaReservasEntities();

        // GET: Tbl_R_Payment
        public ActionResult Index()
        {
            return View(db.Tbl_R_Payment.ToList());
        }

        // GET: Tbl_R_Payment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_R_Payment tbl_R_Payment = db.Tbl_R_Payment.Find(id);
            if (tbl_R_Payment == null)
            {
                return HttpNotFound();
            }
            return View(tbl_R_Payment);
        }

        // GET: Tbl_R_Payment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tbl_R_Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PaymentTypeId,AccountNumberId,PaidDate,Amount,ParcialAmount,Comment,ReservationId,PaymentStatusId,VendedorId,CreatedDate,UpdateDate")] Tbl_R_Payment tbl_R_Payment)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_R_Payment.Add(tbl_R_Payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_R_Payment);
        }

        // GET: Tbl_R_Payment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_R_Payment tbl_R_Payment = db.Tbl_R_Payment.Find(id);
            if (tbl_R_Payment == null)
            {
                return HttpNotFound();
            }
            return View(tbl_R_Payment);
        }

        // POST: Tbl_R_Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PaymentTypeId,AccountNumberId,PaidDate,Amount,ParcialAmount,Comment,ReservationId,PaymentStatusId,VendedorId,CreatedDate,UpdateDate")] Tbl_R_Payment tbl_R_Payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_R_Payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_R_Payment);
        }

        // GET: Tbl_R_Payment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_R_Payment tbl_R_Payment = db.Tbl_R_Payment.Find(id);
            if (tbl_R_Payment == null)
            {
                return HttpNotFound();
            }
            return View(tbl_R_Payment);
        }

        // POST: Tbl_R_Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_R_Payment tbl_R_Payment = db.Tbl_R_Payment.Find(id);
            db.Tbl_R_Payment.Remove(tbl_R_Payment);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalesApp.Models.Entities;

namespace SalesApp.Controllers.Manager
{
    public class PAYMENT_STATUSController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: PAYMENT_STATUS
        public ActionResult Index()
        {
            return View(db.PAYMENT_STATUS.ToList().OrderByDescending(o => o.Description));
        }

        // GET: PAYMENT_STATUS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT_STATUS pAYMENT_STATUS = db.PAYMENT_STATUS.Find(id);
            if (pAYMENT_STATUS == null)
            {
                return HttpNotFound();
            }
            return View(pAYMENT_STATUS);
        }

        // GET: PAYMENT_STATUS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PAYMENT_STATUS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description")] PAYMENT_STATUS pAYMENT_STATUS)
        {
            if (ModelState.IsValid)
            {
                db.PAYMENT_STATUS.Add(pAYMENT_STATUS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pAYMENT_STATUS);
        }

        // GET: PAYMENT_STATUS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT_STATUS pAYMENT_STATUS = db.PAYMENT_STATUS.Find(id);
            if (pAYMENT_STATUS == null)
            {
                return HttpNotFound();
            }
            return View(pAYMENT_STATUS);
        }

        // POST: PAYMENT_STATUS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description")] PAYMENT_STATUS pAYMENT_STATUS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pAYMENT_STATUS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pAYMENT_STATUS);
        }

        // GET: PAYMENT_STATUS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT_STATUS pAYMENT_STATUS = db.PAYMENT_STATUS.Find(id);
            if (pAYMENT_STATUS == null)
            {
                return HttpNotFound();
            }
            return View(pAYMENT_STATUS);
        }

        // POST: PAYMENT_STATUS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PAYMENT_STATUS pAYMENT_STATUS = db.PAYMENT_STATUS.Find(id);
            db.PAYMENT_STATUS.Remove(pAYMENT_STATUS);
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

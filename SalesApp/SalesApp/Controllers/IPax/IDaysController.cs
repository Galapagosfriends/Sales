using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalesApp.Models.Entities;

namespace SalesApp.Controllers.IPax
{
    [Authorize(Roles = "Admin,IPax")]
    public class IDaysController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: IDays
        public ActionResult Index()
        {
            return View(db.I_Days.ToList().OrderByDescending(o => o.Name));
        }

        // GET: IDays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Days i_Days = db.I_Days.Find(id);
            if (i_Days == null)
            {
                return HttpNotFound();
            }
            return View(i_Days);
        }

        // GET: IDays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IDays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Lang,DayCode")] I_Days i_Days)
        {
            if (ModelState.IsValid)
            {
                db.I_Days.Add(i_Days);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(i_Days);
        }

        // GET: IDays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Days i_Days = db.I_Days.Find(id);
            if (i_Days == null)
            {
                return HttpNotFound();
            }
            return View(i_Days);
        }

        // POST: IDays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Lang,DayCode")] I_Days i_Days)
        {
            if (ModelState.IsValid)
            {
                db.Entry(i_Days).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(i_Days);
        }

        // GET: IDays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Days i_Days = db.I_Days.Find(id);
            if (i_Days == null)
            {
                return HttpNotFound();
            }
            return View(i_Days);
        }

        // POST: IDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            I_Days i_Days = db.I_Days.Find(id);
            db.I_Days.Remove(i_Days);
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

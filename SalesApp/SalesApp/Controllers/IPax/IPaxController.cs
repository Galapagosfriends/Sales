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
    public class IPaxController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: IPax
        public ActionResult Index()
        {
            return View(db.I_Pax.ToList().OrderByDescending(o => o.First_Name));
        }

        // GET: IPax/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Pax i_Pax = db.I_Pax.Find(id);
            if (i_Pax == null)
            {
                return HttpNotFound();
            }
            return View(i_Pax);
        }

        // GET: IPax/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IPax/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,First_Name,Last_Name,Birthdate,Passport,CountryId,AdressId,Gender,Email,Created")] I_Pax i_Pax)
        {
            if (ModelState.IsValid)
            {
                db.I_Pax.Add(i_Pax);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(i_Pax);
        }

        // GET: IPax/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Pax i_Pax = db.I_Pax.Find(id);
            if (i_Pax == null)
            {
                return HttpNotFound();
            }
            return View(i_Pax);
        }

        // POST: IPax/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,First_Name,Last_Name,Birthdate,Passport,CountryId,AdressId,Gender,Email,Created")] I_Pax i_Pax)
        {
            if (ModelState.IsValid)
            {
                db.Entry(i_Pax).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(i_Pax);
        }

        // GET: IPax/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_Pax i_Pax = db.I_Pax.Find(id);
            if (i_Pax == null)
            {
                return HttpNotFound();
            }
            return View(i_Pax);
        }

        // POST: IPax/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            I_Pax i_Pax = db.I_Pax.Find(id);
            db.I_Pax.Remove(i_Pax);
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

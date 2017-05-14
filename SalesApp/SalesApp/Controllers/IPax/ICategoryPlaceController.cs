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
    public class ICategoryPlaceController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: I_CategoryPlace
        public ActionResult Index()
        {
            var i_CategoryPlace = db.I_CategoryPlace.Include(i => i.I_Category);
            return View(i_CategoryPlace.ToList().OrderByDescending(o => o.Name));
        }

        // GET: I_CategoryPlace/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_CategoryPlace i_CategoryPlace = db.I_CategoryPlace.Find(id);
            if (i_CategoryPlace == null)
            {
                return HttpNotFound();
            }
            return View(i_CategoryPlace);
        }

        // GET: I_CategoryPlace/Create
        public ActionResult Create()
        {
            ViewBag.ICategoryId = new SelectList(db.I_Category, "Id", "Name");
            return View();
        }

        // POST: I_CategoryPlace/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ICategoryId")] I_CategoryPlace i_CategoryPlace)
        {
            if (ModelState.IsValid)
            {
                db.I_CategoryPlace.Add(i_CategoryPlace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ICategoryId = new SelectList(db.I_Category, "Id", "Name", i_CategoryPlace.ICategoryId);
            return View(i_CategoryPlace);
        }

        // GET: I_CategoryPlace/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_CategoryPlace i_CategoryPlace = db.I_CategoryPlace.Find(id);
            if (i_CategoryPlace == null)
            {
                return HttpNotFound();
            }
            ViewBag.ICategoryId = new SelectList(db.I_Category, "Id", "Name", i_CategoryPlace.ICategoryId);
            return View(i_CategoryPlace);
        }

        // POST: I_CategoryPlace/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ICategoryId")] I_CategoryPlace i_CategoryPlace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(i_CategoryPlace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ICategoryId = new SelectList(db.I_Category, "Id", "Name", i_CategoryPlace.ICategoryId);
            return View(i_CategoryPlace);
        }

        // GET: I_CategoryPlace/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            I_CategoryPlace i_CategoryPlace = db.I_CategoryPlace.Find(id);
            if (i_CategoryPlace == null)
            {
                return HttpNotFound();
            }
            return View(i_CategoryPlace);
        }

        // POST: I_CategoryPlace/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            I_CategoryPlace i_CategoryPlace = db.I_CategoryPlace.Find(id);
            db.I_CategoryPlace.Remove(i_CategoryPlace);
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

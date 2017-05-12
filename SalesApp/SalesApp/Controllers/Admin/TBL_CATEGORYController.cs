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
    public class TBL_CATEGORYController : Controller
    {
        private Galadventure_TrabajosEntities db = new Galadventure_TrabajosEntities();

        // GET: TBL_CATEGORY
        public ActionResult Index()
        {
            return View(db.TBL_CATEGORY.ToList());
        }

        // GET: TBL_CATEGORY/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_CATEGORY tBL_CATEGORY = db.TBL_CATEGORY.Find(id);
            if (tBL_CATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(tBL_CATEGORY);
        }

        [Authorize(Roles = "Admin")]
        // GET: TBL_CATEGORY/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TBL_CATEGORY/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "CA_CATEGORY_ID,CA_CATEGORYNAME")] TBL_CATEGORY tBL_CATEGORY)
        {
            if (ModelState.IsValid)
            {
                db.TBL_CATEGORY.Add(tBL_CATEGORY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tBL_CATEGORY);
        }
        [Authorize(Roles = "Admin")]
        // GET: TBL_CATEGORY/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_CATEGORY tBL_CATEGORY = db.TBL_CATEGORY.Find(id);
            if (tBL_CATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(tBL_CATEGORY);
        }

        // POST: TBL_CATEGORY/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CA_CATEGORY_ID,CA_CATEGORYNAME")] TBL_CATEGORY tBL_CATEGORY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_CATEGORY).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tBL_CATEGORY);
        }

        // GET: TBL_CATEGORY/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_CATEGORY tBL_CATEGORY = db.TBL_CATEGORY.Find(id);
            if (tBL_CATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(tBL_CATEGORY);
        }

        // POST: TBL_CATEGORY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBL_CATEGORY tBL_CATEGORY = db.TBL_CATEGORY.Find(id);
            db.TBL_CATEGORY.Remove(tBL_CATEGORY);
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
